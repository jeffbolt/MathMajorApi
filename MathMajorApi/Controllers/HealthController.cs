using System;
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
			return Ok(new
			{
				Status = "Healthy",
				Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
			});
		}
	}
}