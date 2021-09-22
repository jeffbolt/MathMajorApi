using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;

using System;
using System.IO.Compression;
using System.Linq;

namespace MathMajorApi
{
	public class Startup
	{
		private readonly ILogger<Startup> _logger;

		public Startup(IConfiguration configuration, ILogger<Startup> logger)
		{
			Configuration = configuration;
			_logger = logger;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
		{
			app.UseResponseCompression();
			app.UseCors("AllowAll");
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			if (env.IsDevelopment() || env.IsStaging())
			{
				app.UseDeveloperExceptionPage();
				app.UseOpenApi(options =>
				{
					options.PostProcess = (document, x) =>
					{
						document.Schemes = new[] { OpenApiSchema.Https };
						document.Host = Environment.GetEnvironmentVariable("DOMAIN") ?? "";
					};
				});

				app.UseSwaggerUi3(options =>
				{
					options.Path = "/swagger";
					foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
					{
						string version = $"v{description.ApiVersion.MajorVersion}";
						options.SwaggerRoutes.Add(new SwaggerUi3Route($"{version}", $"/swagger/{version}/swagger.json"));
					}
				});
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});

			services.AddSingleton<IValidationService, ValidationService>();
			services.AddSingleton<IMathService, MathService>();

			ConfigureResponseEncoding(services);
			ConfigureLogging(services);

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyHeader();
					builder.AllowAnyMethod();
				});
			});

			ConfigureOpenApi(services);
			ConfigureVersioning(services);
		}

		private static void ConfigureVersioning(IServiceCollection services)
		{
			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
				options.ApiVersionReader = new UrlSegmentApiVersionReader();
			});

			// Changed in v3.x https://github.com/dotnet/aspnet-api-versioning/issues/330
			services.AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.SubstituteApiVersionInUrl = true;
			});
		}

		private static void ConfigureResponseEncoding(IServiceCollection services)
		{
			services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<BrotliCompressionProvider>();  // Brotli will be chosen first based upon order here
				options.Providers.Add<GzipCompressionProvider>();
			});

			services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
			services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });
		}

		private void ConfigureLogging(IServiceCollection services)
		{
			/* Switching to using "Serilog" log provider for everything
                NOTE: Call to ClearProviders() is what turns off the default Console Logging

                Output to the Console is now controlled by the WriteTo format below
                DevOps can control the Log output with environment variables
                    LOG_MINIMUMLEVEL - values like INFORMATION, WARNING, ERROR
                    LOG_JSON - true means to output log to console in JSON format
            */
			var level = LogLevel.None;
			var serilogLevel = new LoggingLevelSwitch
			{
				MinimumLevel = LogEventLevel.Information
			};

			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOG_MINIMUMLEVEL")))
			{
				Enum.TryParse(Environment.GetEnvironmentVariable("LOG_MINIMUMLEVEL"), out level);
				var eventLevel = LogEventLevel.Information;
				Enum.TryParse(Environment.GetEnvironmentVariable("LOG_MINIMUMLEVEL"), out eventLevel);
				serilogLevel.MinimumLevel = eventLevel;
			}

			bool useJson = Environment.GetEnvironmentVariable("LOG_JSON") == "true";

			var config = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.ReadFrom.Configuration(Configuration);

			if (useJson)
				config.WriteTo.Console(new ElasticsearchJsonFormatter());
			else
				config.WriteTo.Console(outputTemplate: 
					"[{Timestamp:MM-dd-yyyy HH:mm:ss.SSS} {Level:u3}] {Message:lj} {TransactionID}{NewLine}{Exception}",
					theme: SystemConsoleTheme.Literate);

			if (level != LogLevel.None)
				config.MinimumLevel.ControlledBy(serilogLevel);

			Log.Logger = config.CreateLogger();

			services.AddLogging(lb =>
			{
				lb.ClearProviders();
				lb.AddSerilog();
				lb.AddDebug();  // Write to VS Output window (controlled by appsettings "Logging" section)
			});
		}

		private static void ConfigureOpenApi(IServiceCollection services)
		{
			services.AddSwaggerDocument(document =>
			{
				document.Version = "v1";
				document.DocumentName = "v1";
				document.Title = "MathMajor API";
				document.Description = "MathMajor API";
				document.DocumentProcessors.Add(new SecurityDefinitionAppender(
					"ApiKey", Enumerable.Empty<string>(),
					new OpenApiSecurityScheme
					{
						Type = OpenApiSecuritySchemeType.ApiKey,
						Name = "MathMajorApiToken",
						In = OpenApiSecurityApiKeyLocation.Header,
						Description = "An API Key is required."
					}));
			});
		}
	}
}