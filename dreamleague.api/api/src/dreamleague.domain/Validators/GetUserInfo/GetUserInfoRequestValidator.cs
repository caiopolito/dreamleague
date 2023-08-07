using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.shared.Properties;
using FluentValidation;

namespace dreamleague.domain.Validators.GetUserInfo
{
    public class GetUserInfoRequestValidator : AbstractValidator<GetUserInfoRequest>
    {
        public GetUserInfoRequestValidator()
        {
            RuleFor(x => x.SteamId)
                .NotNull()
                .NotEmpty()
                .WithMessage(Resources.RequiredSteamId);
        }
    }
}
