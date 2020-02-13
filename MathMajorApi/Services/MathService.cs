using System;
using System.Collections.Generic;
using System.Linq;

namespace MathMajorApi
{
	public class MathService : IMathService
	{
		// https://en.wikipedia.org/wiki/Fortunate_number
		// https://en.wikipedia.org/wiki/Lucky_number
		// https://en.wikipedia.org/wiki/Harshad_number

		public IEnumerable<double> GetFibonacci(int count)
		{
			return FibonacciD().Take(count);
		}
		private IEnumerable<int> GetFibonacciR(int count)
		{
			var fib = new List<int>();
			FibonacciR(ref fib, count, 1);
			return fib.ToList();
		}

		private void FibonacciR(ref List<int> numbers, int count, int number)
		{
			// Recursive Fibonacci function
			int n = numbers.Count();
			if (n < count)
			{
				if (n == 0)
					numbers.AddRange(new List<int>{ 1, 1 });
				else
					numbers.Add(number);

				n = numbers.Count();
				FibonacciR(ref numbers, count, numbers.ElementAt(n - 1) + numbers.ElementAt(n - 2));
			}
		}

		private IEnumerable<int> Fibonacci()
		{
			int current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}

		private IEnumerable<long> FibonacciL()
		{
			long current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}
		private IEnumerable<ulong> FibonacciUL()
		{
			ulong current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}

		private IEnumerable<double> FibonacciD()
		{
			double current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}

		public bool IsPrime(int number)
		{
			// https://en.wikipedia.org/wiki/Prime_number
			// A number n is prime if it is greater than one and if none of the numbers {2,3,...,n-1} divides n evenly
			if (number <= 1) return false;

			for (int i = 2; i < number - 1; i++)
			{
				if (number % i == 0) return false;
			}

			return true;
		}

		public int Pdi(double number, int _base = 10)
		{
			// A perfect digital invariant (PDI) is a number in a given number base b that is the
			// sum of its own digits each raised to a given power p.
			// For example, the number 4150 in base b=10 is a perfect digital invariant with p=5,
			// because 4150 = 4^5 + 5^5 + 1^5 + 0^5.
			// https://en.wikipedia.org/wiki/Perfect_digital_invariant
			double total = 0;

			while (number > 0)
			{
				total = total + Math.Pow(number % _base, 2);
				number = Math.Floor(number / _base);
			}

			return (int)total;
		}

		public bool IsHappy(int number)
		{
			// https://en.wikipedia.org/wiki/Happy_number#Programming_example
			var seenNumbers = new List<int>();

			while (number > 1 && !seenNumbers.Contains(number))
			{
				seenNumbers.Add(number);
				number = Pdi(number);
			}

			return number == 1;
		}

		public bool IsHappy2(int number, string previous = "")
		{
			// Happy Numbers: https://en.wikipedia.org/wiki/Happy_number
			// Happy Primes: https://oeis.org/A035497
			double sum = 0;
			char[] digits = number.ToString().ToCharArray();

			foreach (char c in digits)
			{
				sum += Math.Pow(Convert.ToDouble(c.ToString()), 2.0);
			}

			if (sum == 1)
				return true;
			else
			{
				// Not happy if its sequence ends in the cycle {4, 16, 37, 58, 89, 145, 42, 20, 4}
				switch (sum)
				{
					case 4:
					case 16:
					case 37:
					case 58:
					case 89:
					case 145:
					case 42:
					case 20:
						previous += sum.ToString() + ",";
						break;
				}

				if (previous.Length >= 26 && previous.Substring(previous.Length - 26, 26)
					.Equals("4,16,37,58,89,145,42,20,4,"))
					return false;

				return IsHappy2((int)sum, previous);
			}
		}

		public bool IsHappyPrime(int number)
		{
			return IsPrime(number) && IsHappy(number);
		}

		public bool IsPalindrome(int number)
		{
			// https://en.wikipedia.org/wiki/Palindromic_number
			return number.ToString().Equals(number.ToString().Reverse());
		}

		public IEnumerable<int> Primes(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count() < count)
			{
				if (IsPrime(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}

		public IEnumerable<int> HappyNumbers(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count() < count)
			{
				if (IsHappy(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}

		public IEnumerable<int> HappyPrimes(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count() < count)
			{
				if (IsHappyPrime(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}

		public IEnumerable<int> Palindromes(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count() < count)
			{
				if (IsPalindrome(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}
	}
}
