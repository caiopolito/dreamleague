using dreamleague.domain.Aggregates.GetUserInfo;
using dreamleague.domain.Validators.GetUserInfo;
using FluentValidation;

namespace dreamleague.api.Configurations
{
    public static class ValidatorsConfig
    {
        public static void ConfigValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<GetUserInfoRequest>, GetUserInfoRequestValidator>();
    }
}
