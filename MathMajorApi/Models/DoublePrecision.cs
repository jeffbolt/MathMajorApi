using System.ComponentModel.DataAnnotations;

namespace MathMajorApi
{
	public class DoublePrecision
	{
		[Precision(1, 15)]
		public int Digits { get; set; }
	}
}
