namespace MathMajorApi.Service.Interfaces
{
	public interface IValidationService
	{
		string GetString(string value, string defaultValue = "");
		bool IsValidApiToken(string token);
	}
}