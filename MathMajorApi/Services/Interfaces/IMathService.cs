using System.Collections.Generic;

namespace MathMajorApi
{
	public interface IMathService
	{
		IEnumerable<double> GetFibonacci(int count);
		IEnumerable<int> HappyNumbers(int count);
		IEnumerable<int> HappyPrimes(int count);
		int Pdi(double number, int _base = 10);
		bool IsHappy(int number);
		bool IsHappy2(int number, string previous = "");
		bool IsHappyPrime(int number);
		bool IsPalindrome(int number);
		bool IsPrime(int number);
		IEnumerable<int> Palindromes(int count);
		IEnumerable<int> Primes(int count);
	}
}