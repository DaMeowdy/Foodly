using System;
using System.Collections.Generic;

namespace foodly.api.Domain;

public partial class Voter
{
    public Guid VoterId { get; set; }
    public string? DiscordID { get; set; }

    public DateTime? TimeVoted { get; set; }

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
