namespace foodly.api.DTO;

public sealed class ApiError
{
    string ErrorMessage { get; init; }
    string ErrorType { get; init; }
    private ApiError(string errorMessage, string errorType)
    {
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }
    public bool Equals(ApiError? obj)
    {
        if (obj is null)
            return false;
        if (ErrorMessage == obj.ErrorMessage && ErrorType == obj.ErrorType)
            return true;

        return false;
    }
    public static ApiError RaiseError(string errMsg, string errType) => new(errMsg, errType);
}
