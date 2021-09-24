using System.Collections.Generic;

namespace MathMajorApi.Service.Services
{
	public class Context
	{
		private int _input;
		private string _output;

		public Context(int input)
		{
			_input = input;
		}

		public int Input
		{
			get { return _input; }
			set { _input = value; }
		}

		public string Output
		{
			get { return _output; }
			set { _output = value; }
		}
	}

	public abstract class Expression
	{
		public abstract void Interpret(Context value);
	}

	public class DecimalToRomaNumeralParser : Expression
	{
		private readonly List<Expression> expressionTree = new List<Expression>()
		{
			new MillionExpression(),
			new HundredThousandExpression(),
			new TenThousandExpression(),
			new ThousandExpression(),
			new HundredExpression(),
			new TenExpression(),
			new OneExpression()
		};

		public override void Interpret(Context value)
		{
			foreach (Expression exp in expressionTree)
			{
				exp.Interpret(value);
			}
		}
	}

	public abstract class TerminalExpression : Expression
	{
		public override void Interpret(Context value)
		{
			while (value.Input - 9 * Multiplier() >= 0)
			{
				value.Output += Nine();
				value.Input -= 9 * Multiplier();
			}
			while (value.Input - 5 * Multiplier() >= 0)
			{
				value.Output += Five();
				value.Input -= 5 * Multiplier();
			}
			while (value.Input - 4 * Multiplier() >= 0)
			{
				value.Output += Four();
				value.Input -= 4 * Multiplier();
			}
			while (value.Input - Multiplier() >= 0)
			{
				value.Output += One();
				value.Input -= Multiplier();
			}
		}

		public abstract string One();

		public abstract string Four();

		public abstract string Five();

		public abstract string Nine();

		public abstract int Multiplier();
	}

	internal class MillionExpression : TerminalExpression
	{
		public override string One()
		{
			return "m";
		}

		public override string Four()
		{
			return "";
		}

		public override string Five()
		{
			return "";
		}

		public override string Nine()
		{
			return "";
		}

		public override int Multiplier()
		{
			return 1000000;
		}
	}

	internal class HundredThousandExpression : TerminalExpression
	{
		public override string One()
		{
			return "c";
		}

		public override string Four()
		{
			return "cd";
		}

		public override string Five()
		{
			return "d";
		}

		public override string Nine()
		{
			return "cm";
		}

		public override int Multiplier()
		{
			return 100000;
		}
	}

	internal class TenThousandExpression : TerminalExpression
	{
		public override string One()
		{
			return "x";
		}

		public override string Four()
		{
			return "xl";
		}

		public override string Five()
		{
			return "l";
		}

		public override string Nine()
		{
			return "xc";
		}

		public override int Multiplier()
		{
			return 10000;
		}
	}

	internal class ThousandExpression : TerminalExpression
	{
		public override string One()
		{
			return "M";
		}

		public override string Four()
		{
			return "Mv";
		}

		public override string Five()
		{
			return "v";
		}

		public override string Nine()
		{
			return "Mx";
		}

		public override int Multiplier()
		{
			return 1000;
		}
	}

	internal class HundredExpression : TerminalExpression
	{
		public override string One()
		{
			return "C";
		}

		public override string Four()
		{
			return "CD";
		}

		public override string Five()
		{
			return "D";
		}

		public override string Nine()
		{
			return "CM";
		}

		public override int Multiplier()
		{
			return 100;
		}
	}

	internal class TenExpression : TerminalExpression
	{
		public override string One()
		{
			return "X";
		}

		public override string Four()
		{
			return "XL";
		}

		public override string Five()
		{
			return "L";
		}

		public override string Nine()
		{
			return "XC";
		}

		public override int Multiplier()
		{
			return 10;
		}
	}

	internal class OneExpression : TerminalExpression
	{
		public override string One()
		{
			return "I";
		}

		public override string Four()
		{
			return "IV";
		}

		public override string Five()
		{
			return "V";
		}

		public override string Nine()
		{
			return "IX";
		}

		public override int Multiplier()
		{
			return 1;
		}
	}
}