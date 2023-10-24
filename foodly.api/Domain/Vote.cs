using System;
using System.Collections.Generic;
using System.Text.Json;

namespace foodly.api.Domain;

public partial class Vote
{
    public Guid VotesId { get; set; }

    public Guid VoterId { get; set; }

    public string Votes { get; set; } = null!;

    public virtual Voter Voter { get; set; } = null!;
}
