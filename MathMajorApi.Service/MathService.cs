using MathMajorApi.Domain;
using MathMajorApi.Service.Interfaces;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace MathMajorApi.Service
{
	public class MathService : IMathService
	{
		// https://en.wikipedia.org/wiki/Fortunate_number
		// https://en.wikipedia.org/wiki/Lucky_number
		// https://en.wikipedia.org/wiki/Harshad_number

		#region Fibonacci
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
			int n = numbers.Count;
			if (n < count)
			{
				if (n == 0)
					numbers.AddRange(new List<int> { 1, 1 });
				else
					numbers.Add(number);

				n = numbers.Count;
				FibonacciR(ref numbers, count, numbers.ElementAt(n - 1) + numbers.ElementAt(n - 2));
			}
		}

		private static IEnumerable<int> Fibonacci()
		{
			int current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}

		private static IEnumerable<long> FibonacciL()
		{
			long current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}
		private static IEnumerable<ulong> FibonacciUL()
		{
			ulong current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}

		private static IEnumerable<double> FibonacciD()
		{
			double current = 1, next = 1;

			while (true)
			{
				yield return current;
				next = current + (current = next);
			}
		}
		#endregion

		#region Prime and Happy Numbers
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
				total += Math.Pow(number % _base, 2);
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

		public IEnumerable<int> Primes(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count < count)
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
			while (numbers.Count < count)
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
			while (numbers.Count < count)
			{
				if (IsHappyPrime(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}
		#endregion

		#region Calculate Pi (https://stackoverflow.com/questions/11677369/)
		public double Pi(int digits)
		{
			return Math.Round(Math.PI, digits);
		}

		public string CalculatePi(int digits)
		{
			digits++;
			uint[] x = new uint[digits * 10 / 3 + 2];
			uint[] r = new uint[digits * 10 / 3 + 2];
			uint[] pi = new uint[digits];

			for (int j = 0; j < x.Length; j++)
				x[j] = 20;

			for (int i = 0; i < digits; i++)
			{
				uint carry = 0;
				for (int j = 0; j < x.Length; j++)
				{
					uint num = (uint)(x.Length - j - 1);
					uint dem = num * 2 + 1;
					x[j] += carry;

					uint q = x[j] / dem;
					r[j] = x[j] % dem;
					carry = q * num;
				}

				pi[i] = x[^1] / 10;  // C# 8.0: Index from end operator ^  x[x.Length - 1] => x[^1]
				r[x.Length - 1] = x[^1] % 10; ;

				for (int j = 0; j < x.Length; j++)
					x[j] = r[j] * 10;
			}

			var result = "";
			uint c = 0;

			for (int i = pi.Length - 1; i >= 0; i--)
			{
				pi[i] += c;
				c = pi[i] / 10;

				result = (pi[i] % 10).ToString() + result;
			}

			result = string.Concat(result.Substring(0, 1), ".", result[1..]); // Range operator result.Substring(1) => result[1..]
			return result;
		}

		// digits = number of digits to calculate;
		// iterations = accuracy (higher the number the more accurate it will be and the longer it will take.)
		/*	Iterations	Value
			1			3.140597029326068
			2			3.141621029325044
			3			3.141591772182196
			4			3.141592682404404
			5			3.141592652615316
			6			3.141592653623556
			7			3.141592653588612
			8			3.141592653589844
			9			3.141592653589812
			10+			3.141592653589812
		*/
		public string ApproximatePi(int digits, int iterations)
		{
			BigInteger bigInteger = 16 * ArcTan1OverX(5, digits).ElementAt(iterations)
				- 4 * ArcTan1OverX(239, digits).ElementAt(iterations);
			return bigInteger.ToString().Insert(1, ".");
		}

		//arctan(x) = x - x^3/3 + x^5/5 - x^7/7 + x^9/9 - ...
		public static IEnumerable<BigInteger> ArcTan1OverX(int x, int digits)
		{
			var mag = BigInteger.Pow(10, digits);
			var sum = BigInteger.Zero;
			bool sign = true;
			for (int i = 1; true; i += 2)
			{
				var cur = mag / (BigInteger.Pow(x, i) * i);
				if (sign)
					sum += cur;
				else
					sum -= cur;
				yield return sum;
				sign = !sign;
			}
		}
		#endregion

		#region Roman Numerals
		public enum RomanNumeral : uint
		{
			M = 1000,
			D = 500,
			C = 100,
			L = 50,
			X = 10,
			V = 5,
			I = 1
		}

		public string IntToRoman(uint number)
		{
			// https://stackoverflow.com/questions/7040289
			// Note: UInt32 range is {0, 4294967295}, so largest number expressable is 4,294,967,295. Negative numbers not allowed.
			if ((number < 0) || (number > uint.MaxValue)) throw new ArgumentOutOfRangeException(nameof(number));
			if (number < 1) return string.Empty;
			if (number >= 1000) return "M" + IntToRoman(number - 1000);
			if (number >= 900) return "CM" + IntToRoman(number - 900);
			if (number >= 500) return "D" + IntToRoman(number - 500);
			if (number >= 400) return "CD" + IntToRoman(number - 400);
			if (number >= 100) return "C" + IntToRoman(number - 100);
			if (number >= 90) return "XC" + IntToRoman(number - 90);
			if (number >= 50) return "L" + IntToRoman(number - 50);
			if (number >= 40) return "XL" + IntToRoman(number - 40);
			if (number >= 10) return "X" + IntToRoman(number - 10);
			if (number >= 9) return "IX" + IntToRoman(number - 9);
			if (number >= 5) return "V" + IntToRoman(number - 5);
			if (number >= 4) return "IV" + IntToRoman(number - 4);
			if (number >= 1) return "I" + IntToRoman(number - 1);
			throw new ArgumentOutOfRangeException(nameof(number));
		}

		public uint RomanToInt(string roman)
		{
			uint prev = 0, cur, total = 0;
			foreach (char c in roman.Reverse().ToArray())
			{
				cur = (uint)Enum.Parse(typeof(RomanNumeral), c.ToString());

				if (cur < prev)
					total -= cur;
				else
					total += cur;

				prev = cur;
			}
			return total;
		}

		#endregion

		#region Other Math Functions
		public double Power(double number, int exponent)
		{
			// Same as Math.Pow
			double result = 1;

			for (int i = 0; i < exponent; i++)
				result *= number;			
			return result;
		}

		public IEnumerable<double> PowersOf(double number, int exponent)
		{
			// Same as Math.Pow
			double result = 1;

			for (int i = 0; i < exponent; i++)
			{
				result *= number;
				yield return result;
			}
		}

		public bool Palindromic(int number)
		{
			// https://en.wikipedia.org/wiki/Palindromic_number
			char[] reversed = number.ToString().Reverse().ToArray();
			var sb = new StringBuilder();
			foreach (char ch in reversed) { sb.Append(ch); }
			return number.ToString().Equals(sb.ToString());
		}

		public IEnumerable<int> Palindromes(int count)
		{
			var numbers = new List<int>();
			int i = 1;
			while (numbers.Count < count)
			{
				if (Palindromic(i)) numbers.Add(i);
				i++;
			}
			return numbers;
		}

		public bool IsBenford(List<double> numbers)
		{
			//int[numbers.Length - 1] leadingDigits;
			var leadingDigits = new List<double>(); //= new Array(numbers.Length - 1);

			for (int i = 0; i < numbers.Count - 1; i++)
			{
				int first = int.Parse(numbers[i].ToString().Substring(0, 1));
				//leadingDigits[i] = first;
				leadingDigits.Add(first);
			}

			var count = new List<int>();

			for (int i = 1; i <= 9; i++)
			{
				count.Add(leadingDigits.Count(x => x == i));
				Debug.WriteLine("Count of {0}: {1}, Ratio: {2}", i, count[i], (count[i] / 9).ToString("P", CultureInfo.InvariantCulture));
			}

			return true;
		}

		public (double x1, double x2) SolveQuadratic(double a, double b, double c)
		{
			/* Find the roots of the equation ax^2 + bx + c = 0 => x = (-b ִ± √(b^2 - 4ac)) / 2a
				https://owlcation.com/stem/How-to-Find-the-Roots-of-A-Quadratic-Function
				https://en.wikipedia.org/wiki/Quadratic_formula
				https://mathnovice.com/root-types-quadratic-equation-examples-graphs/
					If b^2 - 4ac = 0, one real root (may be irrational)
					If b^2 - 4ac > 0, two real roots
					If b^2 - 4ac < 0, two imaginary roots
				TODO: return Complex numbers?
				https://docs.microsoft.com/en-us/dotnet/api/system.numerics.complex
			*/
			double x1 = (-b + Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / 2 * a;
			double x2 = (-b - Math.Sqrt(Math.Pow(b, 2) - 4 * a * c)) / 2 * a;
			return (x1, x2);
		}

		public double SummationNaturalNumbers(int n)
		{
			// https://en.wikipedia.org/wiki/Summation
			// https://en.wikipedia.org/wiki/Natural_number
			// Σ = n(n + 1) / 2
			return n * (n + 1) / 2;
		}

		public double SummationNaturalNumbersR(int start, int end, double sum)
		{
			// https://en.wikipedia.org/wiki/Summation
			// https://en.wikipedia.org/wiki/Natural_number
			// Σ = n(n + 1) / 2
			if (start >= end)
				return sum;
			else
				return SummationNaturalNumbersR(++start, end, sum + 1);
		}

		public double Compound(double principle, double rate, int count, int iterations)
		{
			if (count >= iterations)
				return principle;
			else
				return Compound(principle + (principle * rate), rate, ++count, iterations);
		}

		public double CompoundDouble(double number, int current, int iterations)
		{
			if (current >= iterations)
				return number;
			else
				return CompoundDouble(number + number * 2, ++current, iterations);
		}

		public List<MatrixElement> EncodeTapCode(string input)
		{
			var tapCode = new TapCode();
			return tapCode.Encode(input);
		}

		public string ToHex(long value)
		{
			return string.Format("{0:x}", value);
			//return value.ToString("X");
		}
		
		public long FromHex(string value)
		{
			return long.Parse(value, NumberStyles.HexNumber);
			//return Convert.ToInt64(value, 16);
		}
		#endregion
	}
}
