using foodly.api.DTO;

namespace foodly.api.Services;

public interface IVotingService
{
    public Task<CreateVoteResponse> CreateVoteAsync(CreateVoteRequest request);
    public Task<bool> VerifyVoterAsync(string discordId);
}