using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSwag;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;

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

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options => options.EnableEndpointRouting = false);

			services.AddApiVersioning(options =>
			{
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.ReportApiVersions = true;
			});

			services.AddSingleton<IValidationService, ValidationService>();
			services.AddSingleton<IMathService, MathService>();

			services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyHeader();
					builder.AllowAnyMethod();
				});
			});

			services.AddSwaggerDocument(document =>
			{
				document.DocumentName = "swagger";
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

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment() || env.IsStaging())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseCors("AllowAll");
			app.UseHttpsRedirection();
			app.UseMvc();

			app.UseOpenApi(options =>
			{
				options.DocumentName = "swagger";
				options.Path = "/swagger/v1/swagger.json";
				options.PostProcess = (document, x) =>
				{
					document.Schemes = new[] { OpenApiSchema.Https };
					if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DOMAIN")))
					{
						document.Host = Environment.GetEnvironmentVariable("DOMAIN");
					}
				};
			});

			app.UseSwaggerUi3(options =>
			{
				options.Path = "/swagger";
				options.DocumentPath = "swagger/v1/swagger.json";
				options.SwaggerRoutes.Add(new SwaggerUi3Route("v1", "/swagger/v1/swagger.json"));
			});
		}
	}
}