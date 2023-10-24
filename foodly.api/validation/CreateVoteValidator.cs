using FluentValidation;
using foodly.api.DTO;

namespace foodly.api.Validation;

public class CreateVoteRequestValidator : AbstractValidator<CreateVoteRequest>
{
    public CreateVoteRequestValidator()
    {
        RuleFor(x => x.DiscordID).NotEmpty().WithMessage("Missing Discord ID in request");
        RuleFor(x => x.votes.Length).Equal(2);
        RuleFor(x => x.vego_votes.Length).Equal(2);
    }
}