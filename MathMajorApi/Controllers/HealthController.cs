using System;
using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Mvc;

namespace MathMajorApi
{
	[Route("api/v{version:apiVersion}/Health")]
	[ApiController]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	public class HealthController : Controller
	{
		[HttpGet]
		[ProducesResponseType(typeof(HealthResponse), 200)]
		public IActionResult Index()
		{
			var assembly = Assembly.GetExecutingAssembly();
			return Ok(new
			{
				Status = "Healthy",
				Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
				AssemblyVersion = assembly.GetName().Version.ToString(),
				FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion
			});
		}
	}
}