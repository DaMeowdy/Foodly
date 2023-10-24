using System.Text.Json;
using FluentValidation;
using foodly.api.Domain;
using foodly.api.DTO;
using foodly.api.Persistence;
using foodly.api.Validation;
using Microsoft.EntityFrameworkCore;

namespace foodly.api.Services;
public class VotingService : IVotingService
{
    private readonly FoodlyContext _context;
    public VotingService(FoodlyContext context)
    {
        _context = context;
    }

    public async Task<CreateVoteResponse> CreateVoteAsync(CreateVoteRequest request)
    {
        if (!await VerifyVoterAsync(request.DiscordID))
        {
            var response = new CreateVoteResponse("Vote Failed");
            response.AddError($"User {request.DiscordID} has already voted", "attempted duplicate vote");
            return response;
        }

        if (!_context.Voters.Any(voter => voter.DiscordID == request.DiscordID))
        {
            Guid guid = Guid.NewGuid();
            Vote vote = new Vote
            {
                Votes = JsonSerializer.Serialize(request.vego_votes.Concat(request.votes)),
                VoterId = guid,

            };
            Voter voter = new Voter
            {
                VoterId = guid,
                DiscordID = request.DiscordID,
                TimeVoted = DateTime.Now,
                Votes = new List<Vote>()
            };
            voter.Votes.Add(vote);
            await _context.Voters.AddAsync(voter);
            await _context.SaveChangesAsync();
            return new CreateVoteResponse("Voted Successfully");
        }
        else
        {
            Voter? _voter = await _context.Voters.SingleOrDefaultAsync(voter => voter.DiscordID == request.DiscordID);
            if (_voter is null)
            {
                return new CreateVoteResponse("internal server error");
            }
            Vote vote = new Vote
            {
                Votes = JsonSerializer.Serialize(request.vego_votes.Concat(request.votes)),
                VoterId = _voter.VoterId,

            };
            _voter.Votes.Add(vote);
            _context.Entry(_voter).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new CreateVoteResponse("Voted Successfully");
        }
    }

    public async Task<bool> VerifyVoterAsync(string discordId)
    {
        Voter? voter = await _context.Voters.SingleOrDefaultAsync(voter => voter.DiscordID == discordId);
        if (voter is null)
        {
            return true;
        }
        Vote? vote = await _context.Votes.SingleOrDefaultAsync(votes => votes.VoterId == voter.VoterId);
        if (vote is not null)
        {
            return false;
        }
        else
            return true;
    }
}