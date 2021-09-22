using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathMajorApi
{
	// https://en.wikipedia.org/wiki/Tap_code
	public class MatrixElement
	{
		public int Row { get; set; }
		public int Column { get; set; }
		public string Letter { get; set; }

		public MatrixElement() { }

		public MatrixElement(int row, int column, string letter)
		{
			Row = row;
			Column = column;
			Letter = letter;
		}
	}

	public class TapCode
	{
		private readonly List<MatrixElement> Matrix = new();

		public TapCode()
		{
			for (int row = 1; row <= 5; row++)
			{
				for (int column = 1; column <= 5; column++)
				{
					string letter = ((char)('A' - 2 + row + column)).ToString();
					if (letter == "C") letter = "C/K";
					Matrix.Add(new MatrixElement(row, column, letter));
				}
			}
		}

		public string LetterAt(int row, int column)
		{
			return Matrix.FirstOrDefault(x => x.Row == row && x.Column == column)?.Letter;
		}

		public MatrixElement ElementAt(string letter)
		{
			return Matrix.FirstOrDefault(x => x.Letter == letter);
		}

		public List<MatrixElement> Encode(string input)
		{
			var encoded = new List<MatrixElement>();
			foreach (char c in input.ToCharArray())
			{
				// The letter "X" is used to break up sentences, and "K" for acknowledgements
				string s;
				switch (c.ToString().ToUpper())
				{
					case "X":
						s = " ";
						break;
					case "K":
						s = Environment.NewLine;
						break;
					default:
						encoded.Add(ElementAt(c.ToString()));
						break;
				}
				//encoded.Add(s);
			}
			return encoded;
		}

		public string Decode(List<MatrixElement> encoded)
		{
			var decoded = new StringBuilder();
			foreach(var e in encoded)
			{
				decoded.Append(LetterAt(e.Row, e.Column));
			}
			return decoded.ToString();
		}
	}
}