using MathMajorApi.Service;
using MathMajorApi.Service.Interfaces;

using System;
using System.Collections.Generic;

using Xunit;

namespace MathMajorApi.Tests
{
	public class MathServiceTests
	{
		private readonly IMathService service;

		public MathServiceTests()
		{
			service = new MathService();
		}

		public static IEnumerable<object[]> GetAccountFibinacciTestData()
		{
			var testData = new List<object[]>
			{
				new object[] { 1, new List<double> { 1 } },
				new object[] { 2, new List<double> { 1, 1 } },
				new object[] { 3, new List<double> { 1, 1, 2 } },
				new object[] { 4, new List<double> { 1, 1, 2, 3 } },
				new object[] { 5, new List<double> { 1, 1, 2, 3, 5 } },
				new object[] { 6, new List<double> { 1, 1, 2, 3, 5, 8 } },
				new object[] { 7, new List<double> { 1, 1, 2, 3, 5, 8, 13 } },
				new object[] { 8, new List<double> { 1, 1, 2, 3, 5, 8, 13, 21 } },
				new object[] { 9, new List<double> { 1, 1, 2, 3, 5, 8, 13, 21, 34 } },
				new object[] { 10, new List<double> { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55 } }
			};

			return testData;
		}

		[Theory]
		[MemberData(nameof(GetAccountFibinacciTestData))]
		public void FibinacciTest(int count, IEnumerable<double> expected)
		{
			var actual = service.GetFibonacci(count);
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void CalculatePi_IsAccuracte()
		{
			string calculated = service.CalculatePi(16);        // 3.1415926535897932
			double actual = Convert.ToDouble(calculated);       // 3.1415926535897931 (rounding bug?)
			double expected = Math.PI;                          // 3.1415926535897931
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(0, "3.")]
		[InlineData(1, "3.1")]
		[InlineData(2, "3.14")]
		[InlineData(3, "3.141")]
		[InlineData(4, "3.1415")]
		[InlineData(5, "3.14159")]
		[InlineData(6, "3.141592")]
		[InlineData(7, "3.1415926")]
		[InlineData(8, "3.14159265")]
		[InlineData(9, "3.141592653")]
		[InlineData(10, "3.1415926535")]
		[InlineData(11, "3.14159265358")]
		[InlineData(12, "3.141592653589")]
		[InlineData(13, "3.1415926535897")]
		[InlineData(14, "3.14159265358979")]
		[InlineData(15, "3.141592653589793")]
		[InlineData(16, "3.1415926535897932")]
		[InlineData(17, "3.14159265358979323")]
		[InlineData(18, "3.141592653589793238")]
		[InlineData(19, "3.1415926535897932384")]
		[InlineData(20, "3.14159265358979323846")]
		[InlineData(21, "3.141592653589793238462")]
		[InlineData(22, "3.1415926535897932384626")]
		[InlineData(23, "3.14159265358979323846264")]
		[InlineData(24, "3.141592653589793238462643")]
		[InlineData(25, "3.1415926535897932384626433")]
		public void CalculatePiTest(int digits, string expected)
		{
			string actual = service.CalculatePi(digits);
			Assert.Equal(expected, actual);
		}

		[Theory]
		[InlineData(15, 0, "3.183263598326360")]
		[InlineData(15, 1, "3.140597029326068")]
		[InlineData(15, 2, "3.141621029325044")]
		[InlineData(15, 3, "3.141591772182196")]
		[InlineData(15, 4, "3.141592682404404")]
		[InlineData(15, 5, "3.141592652615316")]
		[InlineData(15, 6, "3.141592653623556")]
		[InlineData(15, 7, "3.141592653588612")]
		[InlineData(15, 8, "3.141592653589844")]
		[InlineData(15, 9, "3.141592653589812")]
		public void ApproximatePiTest(int digits, int iterations, string expected)
		{
			string actual = service.ApproximatePi(digits, iterations);
			Assert.Equal(expected, actual);
		}
	}
}
