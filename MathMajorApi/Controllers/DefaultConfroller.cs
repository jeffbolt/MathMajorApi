using System.Collections.Generic;
using System.Numerics;
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

		[HttpGet("fibinacci")]
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

		[HttpGet("isprime")]
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

		[HttpGet("ishappy")]
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

		[HttpGet("ishappyprime")]
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

		[HttpGet("primes")]
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

		[HttpGet("pdi")]
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

		[HttpGet("happynumbers")]
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

		[HttpGet("happyprimes")]
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

		[HttpGet("ispalindromic")]
		[ProducesResponseType(200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsPalindromic(int number)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();
			else if (number < int.MinValue || number > int.MaxValue)
				return BadRequest(string.Format("The number must be an integer value between {0} and {1}.",
					int.MinValue, int.MaxValue));

			return Ok(_mathService.Palindromic(number));
		}

		[HttpPost("isbenford")]
		[ProducesResponseType(typeof(List<double>), 200)]
		[ProducesResponseType(401)]
		[ProducesResponseType(404)]
		public IActionResult IsBenford(List<double> numbers)
		{
			if (!_validationService.IsValidApiToken(GetApiToken()))
				return Unauthorized();

			return Ok(_mathService.IsBenford(numbers));
		}

		[HttpGet("Pi")]
		[ProducesResponseType(typeof(double), 200)]
		[ProducesResponseType(400)]
		public IActionResult Pi(int digits)
		{
			if (digits < 1 || digits > 15)
				return BadRequest("The digits field must be between 1 and 15");
			else if (!ModelState.IsValid)
				return BadRequest(ModelState);
			else
				return Ok(_mathService.Pi(digits));
		}

		[HttpPost("Pi")]
		[ProducesResponseType(typeof(double), 200)]
		[ProducesResponseType(400)]
		public IActionResult Pi(DoublePrecision precision)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			else
				return Ok(_mathService.Pi(precision.Digits));
		}

		[HttpGet("CalculatePi")]
		[ProducesResponseType(typeof(string), 200)]
		public IActionResult CalculatePi(int digits)
		{
			return Ok(_mathService.CalculatePi(digits));
		}

		[HttpGet("ApproximatePi")]
		[ProducesResponseType(typeof(BigInteger), 200)]
		public IActionResult CalculatePi(int digits, int iterations)
		{
			return Ok(_mathService.ApproximatePi(digits, iterations));
		}
	}
}