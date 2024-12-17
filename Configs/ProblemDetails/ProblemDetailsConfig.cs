using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Simu.Api.Shared;

namespace Simu.Api.Configs.ProblemDetails;

public static class ProblemDetailsConfig
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (ctx, _) => false;
            options.Map<ValidationException>(ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = 400,
                Detail = string.Join(',', ex.Errors.Select(e => e.ErrorMessage))
            });
            options.Map<BusinessException>(ex => new Microsoft.AspNetCore.Mvc.ProblemDetails
            {
                Title = "One or more business errors occurred.",
                Status = 400,
                Detail = ex.Message
            });
            options.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
        });
        return services;
    }
}