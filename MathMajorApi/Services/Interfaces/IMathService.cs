using System.Collections.Generic;
using System.Numerics;

namespace MathMajorApi
{
	public interface IMathService
	{
		IEnumerable<double> GetFibonacci(int count);
		IEnumerable<int> HappyNumbers(int count);
		IEnumerable<int> HappyPrimes(int count);
		bool IsHappy(int number);
		bool IsHappy2(int number, string previous = "");
		bool IsHappyPrime(int number);
		bool Palindromic(int number);
		bool IsPrime(int number);
		IEnumerable<int> Palindromes(int count);
		IEnumerable<int> Primes(int count);
		double Pi(int digits);
		string CalculatePi(int digits);
		BigInteger ApproximatePi(int digits, int iterations);
		int Pdi(double number, int _base = 10);
		bool IsBenford(List<double> numbers);
	}
}