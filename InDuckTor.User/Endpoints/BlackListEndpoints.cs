using FluentValidation;
using InDuckTor.User.Features.BlackList.CreateBan;
using InDuckTor.User.Features.BlackList.DeleteBan;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.WebApi.Validation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InDuckTor.User.WebApi.Endpoints
{
    public record BanUser(DateTime? EndDate, string? Reason);

    public class BanUserValidator : AbstractValidator<BanUser>
    {
        public BanUserValidator()
        {
            RuleFor(r => r.EndDate).GreaterThan(DateTime.UtcNow);

        }
    }

        public static class BlackListEndpoints
    {
        public static IEndpointRouteBuilder AddBlackListEndpoints(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("/api/v1")
                .WithTags("BlackList")
                .WithOpenApi()
                .RequireAuthorization();

            groupBuilder.MapPost("/ban/{userId}", CreateBan)
                .AddEndpointFilter<ValidationFilter<BanUser>>(); ;

            groupBuilder.MapDelete("/ban/{userId}", CancelBan)
               .WithDescription("Разблокировать пользователя");

            return builder;
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        internal static async Task<IResult> CreateBan([FromRoute] long userId, [FromBody] BanUser request,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var transformedRequest = request.Adapt<CreateBanRequest>() with { UserId = userId };
            await mediator.Send(new CreateBanCommand(transformedRequest), cancellationToken);
            return Results.Ok();
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        internal static async Task<IResult> CancelBan([FromRoute] long userId,
           [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteBanCommand(userId), cancellationToken);
            return Results.Ok();
        }
    }
}
