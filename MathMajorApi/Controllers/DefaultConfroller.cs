using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MathMajorApi
{
	[Route("api/v{version:apiVersion}")]
	[ApiController]
	[Produces("application/json")]
	[ApiVersion("1.0")]
	public class DefaultController : Controller
	{
		private readonly IValidationService _validationService;
		private readonly IMathService _mathService;

		public DefaultController(IValidationService validationService, IMathService mathService)
		{
			_validationService = validationService;
			_mathService = mathService;
		}

		private string GetApiToken()
		{
			return Request.Headers["MathMajorApiToken"].ToString();
		}

		[Route("fibinacci")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<double>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult Fibinacci(int count)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (count < Constants.MinFibonacciValue || count > Constants.MaxFibonacciDoubleValue)
				return BadRequest(string.Format("The count must be an integer value between {0} and {1}.",
					Constants.MinFibonacciValue, Constants.MaxFibonacciDoubleValue));

			var fibonacci = _mathService.GetFibonacci(count);
			return Ok(fibonacci);
		}

		[Route("isprime")]
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsPrime(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < 2 || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					2, int.MaxValue));

			return Ok(_mathService.IsPrime(number));
		}

		[Route("ishappy")]
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsHappy(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < 1 || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					1, int.MaxValue));

			return Ok(_mathService.IsHappy(number));
		}

		[Route("ishappyprime")]
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsHappyPrime(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < 2 || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					2, int.MaxValue));

			return Ok(_mathService.IsHappyPrime(number));
		}

		[Route("primes")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<int>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult Primes(int count)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (count < 1 || count > int.MaxValue)
				return BadRequest(string.Format("The count must be an integer value between {0} and {1}.",
					1, int.MaxValue));

			return Ok(_mathService.Primes(count));
		}

		[Route("pdi")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<int>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult Pdi(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < 1 || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					1, int.MaxValue));

			return Ok(_mathService.Pdi(number));
		}

		[Route("happynumbers")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<int>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult HappyNumbers(int count)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (count < 1 || count > int.MaxValue)
				return BadRequest(string.Format("The count must be an integer value between {0} and {1}.",
					1, int.MaxValue));

			return Ok(_mathService.HappyNumbers(count));
		}

		[Route("happyprimes")]
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<int>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult HappyPrimes(int count)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (count < 1 || count > int.MaxValue)
				return BadRequest(string.Format("The count must be an integer value between {0} and {1}.",
					2, int.MaxValue));

			return Ok(_mathService.HappyPrimes(count));
		}

		[Route("ispalindrome")]
		[HttpGet]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsPalindrome(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < int.MinValue || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					int.MinValue, int.MaxValue));

			return Ok(_mathService.IsPalindrome(number));
		}
	}
}