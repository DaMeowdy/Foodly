namespace foodly.api.DTO;
public sealed record CreateVoteRequest(string DiscordID, string[] vego_votes, string[] votes);