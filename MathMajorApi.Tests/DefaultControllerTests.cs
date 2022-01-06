using FluentAssertions;

using MathMajorApi.Service.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using NSubstitute;

using System.Collections.Generic;

using Xunit;

namespace MathMajorApi.Tests
{
	public class DefaultControllerTests
	{
		private readonly DefaultController controller;
		private readonly IValidationService validationService;
		private readonly IMathService mathService;

		public DefaultControllerTests()
		{
			validationService = Substitute.For<IValidationService>();
			mathService = Substitute.For<IMathService>();
			controller = new DefaultController(validationService, mathService)
			{
				ControllerContext = new ControllerContext
				{
					HttpContext = new DefaultHttpContext()
				}
			};
		}

		[Fact]
		public void Fibinacci_ReturnsOk()
		{
			var expected = new List<double>
			{
				1, 1, 2, 3, 5, 8, 13, 21, 34, 55
			};
			validationService.IsValidApiToken(Arg.Any<string>()).Returns(true);
			mathService.GetFibonacci(Arg.Any<int>()).Returns(expected);

			var response = controller.Fibinacci(10);

			Assert.IsType<OkObjectResult>(response);
			var value = ((OkObjectResult)response).Value;
			Assert.IsType<List<double>>(value);
			((List<double>)value).Should().BeEquivalentTo(expected);
		}
	}
}
