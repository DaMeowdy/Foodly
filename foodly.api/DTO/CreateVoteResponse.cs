namespace foodly.api.DTO;

public sealed class CreateVoteResponse
{
    public string Status { get; init; }
    public List<ApiError> Errors { get; private set; }
    public CreateVoteResponse(string status)
    {
        Status = status;
        Errors = new List<ApiError>();
    }
    public void AddError(string errorMessage, string errorType)
    {
        ApiError _err = ApiError.RaiseError(errorMessage, errorType);
        if (!this.Errors.Any(err => err.Equals(_err)))
            this.Errors.Add(_err);
        return;
    }
}
