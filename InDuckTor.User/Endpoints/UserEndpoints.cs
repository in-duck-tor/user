using InDuckTor.User.Features.Client.GetAllClients;
using InDuckTor.User.Features.User.GetUserClaims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InDuckTor.User.WebApi.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder AddUserEndpoints(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("/api/v1")
                .WithTags("User")
                .WithOpenApi()
                .RequireAuthorization();

            groupBuilder.MapGet("/user/{id}/claims", GetUserClaims)
                .WithDescription("Получить информацию о пользователе для клеймов");

            return builder;
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType<UserClaimsDto>(200)]
        internal static async Task<IResult> GetUserClaims([FromRoute] long id,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUserClaimsQuery(id), cancellationToken);
            return Results.Ok(result);
        }
    }
}