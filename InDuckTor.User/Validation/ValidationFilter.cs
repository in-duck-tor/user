
using FluentValidation;

namespace InDuckTor.User.WebApi.Validation
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is not null)
            {
                var request = context.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T));

                if (request is not null)
                {
                    var validation = await validator.ValidateAsync(request);
                    if (validation.IsValid)
                    {
                        return await next(context);
                    }
                    return Results.ValidationProblem(validation.ToDictionary());
                }
                else
                {
                    return Results.Problem("Could not find type to validate");
                }
                
            }
            return await next(context);
        }
    }
}
