using MathMajorApi.Domain;
using MathMajorApi.Service.Interfaces;

using System.Diagnostics;

namespace MathMajorApi.Service
{
	public class ValidationService : IValidationService
	{
		public bool IsValidApiToken(string token)
		{
			if (Debugger.IsAttached)
				return true;  // Bypass token in debug mode
			else
				return (token == Constants.ApiToken);
		}

		public string GetString(string value, string defaultValue = "")
		{
			return string.IsNullOrEmpty(value) ? defaultValue : value;
		}
	}
}
