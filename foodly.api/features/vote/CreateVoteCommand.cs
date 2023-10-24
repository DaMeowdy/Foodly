using foodly.api.DTO;
using foodly.api.Services;
using MediatR;

namespace foodly.api.features.vote;

public record CreateVoteCommand(CreateVoteRequest voteRequest) : IRequest<CreateVoteResponse>;

public class CreateVoteHandler : IRequestHandler<CreateVoteCommand, CreateVoteResponse>
{
    private IVotingService _votingService;
    public CreateVoteHandler(IVotingService votingService)
    {
        this._votingService = votingService;
    }
    public async Task<CreateVoteResponse> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        CreateVoteResponse response = await _votingService.CreateVoteAsync(request.voteRequest);
        return response;
    }
}