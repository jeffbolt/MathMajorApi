using MathMajorApi.Domain;

using System.Collections.Generic;

namespace MathMajorApi.Service.Interfaces
{
	public interface IMathService
	{
		IEnumerable<double> GetFibonacci(int count);
		IEnumerable<int> HappyNumbers(int count);
		IEnumerable<int> HappyPrimes(int count);
		bool IsHappy(int number);
		bool IsHappy2(int number, string previous = "");
		bool IsHappyPrime(int number);
		double Power(double number, int exponent);
		IEnumerable<double> PowersOf(double number, int exponent);
		bool Palindromic(int number);
		bool IsPrime(int number);
		IEnumerable<int> Palindromes(int count);
		IEnumerable<int> Primes(int count);
		double Pi(int digits);
		string CalculatePi(int digits);
		string ApproximatePi(int digits, int iterations);
		int Pdi(double number, int _base = 10);
		bool IsBenford(List<double> numbers);
		(double x1, double x2) SolveQuadratic(double a, double b, double c);
		double SummationNaturalNumbers(int n);
		double SummationNaturalNumbersR(int start, int end, double sum);
		double Compound(double principle, double rate, int count, int iterations);
		double CompoundDouble(double number, int current, int iterations);
		string IntToRoman(uint number);
		uint RomanToInt(string roman);
		List<MatrixElement> EncodeTapCode(string input);
		string ToHex(long value);
		long FromHex(string value);
	}
}