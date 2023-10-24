using foodly.api.DTO;
using foodly.api.features.food;
using foodly.api.features.vote;
using MediatR;
using Microsoft.AspNetCore.Mvc;

public static class VotingEndpoints
{
    public static void MapVotingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/createVote", async (ISender sender, [FromBody] CreateVoteRequest request
        ) =>
        {
            CreateVoteCommand CreateVoteCommand = new(request);
            CreateVoteResponse response = await sender.Send(CreateVoteCommand, new CancellationToken());
            return response;
        });
    }
}