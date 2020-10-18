using System;
using System.ComponentModel.DataAnnotations;

namespace MathMajorApi
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class PrecisionAttribute : DataTypeAttribute
	{
		//public MinLengthAttribute minLengthAttribute; //= new MinLengthAttribute(1);
		//public MaxLengthAttribute maxLengthAttribute; //= new MaxLengthAttribute(15);
		private readonly int _minLength;
		private readonly int _maxLength;

		public PrecisionAttribute(int minLength, int maxLength) : base(DataType.Text)
		{
			_minLength = minLength;
			_maxLength = maxLength;
			//minLengthAttribute = new MinLengthAttribute(minLength);
			//maxLengthAttribute = new MaxLengthAttribute(maxLength);
		}

		public override bool IsValid(object value)
		{
			if (!(value is int valueAsInt)) return false;
			//return valueAsInt >= minLengthAttribute.Length && valueAsInt <= maxLengthAttribute.Length;
			return valueAsInt >= _minLength && valueAsInt <= _maxLength;
		}
	}
}
