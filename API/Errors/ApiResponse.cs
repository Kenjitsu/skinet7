namespace API.Errors;

public class ApiResponse
{
	public ApiResponse(int statusCode, string message = null)
	{
		StatusCode = statusCode;
		Message = message ?? GetDefualtMessageForStatusCode(statusCode);
	}

	public int StatusCode { get; set; }
	public string Message { get; set; }

	public string GetDefualtMessageForStatusCode(int statusCode)
	{
		return statusCode switch
		{
			400 => "A bad request you have made.",
			401 => "Authorized you are not.",
			404 => "Resource found, it was not.",
			500 => "Errors are a path to a dark side. Errors lead to anger. Anger leads to hate. Hate lead to career change.",
			_ => null,
		};
	}
}
