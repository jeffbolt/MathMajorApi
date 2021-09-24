using MathMajorApi.Domain;
using MathMajorApi.Service.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Numerics;

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

		[HttpGet("Fibinacci")]
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

		[HttpGet("IsPrime")]
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

		[HttpGet("IsHappy")]
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

		[HttpGet("IsHappyPrime")]
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

		[HttpGet("Primes")]
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

		[HttpGet("Pdi")]
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

		[HttpGet("HappyNumbers")]
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

		[HttpGet("HappyPrimes")]
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

		[HttpGet("IsPalindromic")]
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

		[HttpPost("IsBenford")]
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
		public IActionResult ApproximatePi(int digits, int iterations)
		{
			return Ok(_mathService.ApproximatePi(digits, iterations));
		}

		[HttpGet("Power")]
		[ProducesResponseType(typeof(double), 200)]
		public IActionResult Power(double number, int exponent)
		{
			return Ok(_mathService.Power(number, exponent));
		}

		[HttpGet("PowersOf")]
		[ProducesResponseType(typeof(IEnumerable<double>), 200)]
		public IActionResult PowersOf(double number, int exponent)
		{
			return Ok(_mathService.PowersOf(number, exponent));
		}

		[HttpGet("SolveQuadratic")]
		[ProducesResponseType(typeof((double x1, double x2)), 200)]
		public IActionResult SolveQuadratic(double a, double b, double c)
		{
			return Ok(_mathService.SolveQuadratic(a, b, c));
		}

		[HttpGet("SummationNaturalNumbers")]
		[ProducesResponseType(typeof(double), 200)]
		public IActionResult SummationNaturalNumbers(int n)
		{
			return Ok(_mathService.SummationNaturalNumbers(n));
		}

		[HttpGet("SummationNaturalNumbersR")]
		[ProducesResponseType(typeof(double), 200)]
		public IActionResult SummationNaturalNumbersR(int start, int end)
		{
			return Ok(_mathService.SummationNaturalNumbersR(start, end, 0));
		}

		[HttpGet("Compound")]
		[ProducesResponseType(typeof(double), 200)]
		public IActionResult Compound(double principle, double rate, int iterations)
		{
			return Ok(_mathService.Compound(principle, rate, 1, iterations));
		}

		[HttpGet("CompoundDouble")]
		[ProducesResponseType(typeof(double), 200)]
		public IActionResult CompoundDouble(double number, int iterations)
		{
			return Ok(_mathService.CompoundDouble(number, 1, iterations));
		}

		[HttpGet("IntToRoman")]
		[ProducesResponseType(typeof(uint), 200)]
		public IActionResult IntToRoman(uint number)
		{
			return Ok(_mathService.IntToRoman(number));
		}

		[HttpGet("RomanToInt")]
		[ProducesResponseType(typeof(string), 200)]
		public IActionResult RomanToInt(string roman)
		{
			roman = roman.Trim().ToUpper();
			return Ok(_mathService.RomanToInt(roman));
		}

		[HttpPost("TapCode/Encode")]
		[ProducesResponseType(typeof(List<MatrixElement>), 200)]
		public IActionResult EncodeTapCode(string input)
		{
			input = input.Trim().ToUpper();
			return Ok(_mathService.EncodeTapCode(input));
		}

		[HttpGet("ToHex")]
		[ProducesResponseType(typeof(string), 200)]
		public IActionResult ToHex(long value)
		{
			return Ok(_mathService.ToHex(value));
		}

		[HttpGet("FromHex")]
		[ProducesResponseType(typeof(long), 200)]
		public IActionResult FromHex(string value)
		{
			return Ok(_mathService.FromHex(value));
		}
	}
}