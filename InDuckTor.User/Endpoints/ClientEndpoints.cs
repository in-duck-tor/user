using FluentResults;
using InDuckTor.Shared.Strategies;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.WebApi.Mapping;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InDuckTor.User.WebApi.Endpoints
{
    public static class ClientEndpoints
    {
        public static IEndpointRouteBuilder AddClientEndpoints(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("/api/v1")
                .WithTags("Client")
                .WithOpenApi()
                .RequireAuthorization();

            groupBuilder.MapGet("/client", GetAllClients)
                .WithDescription("Получить краткую информацю о всех клиентах");

            groupBuilder.MapPost("/client", CreateClient)
                .WithDescription("Создать клиента");

            return builder;
        }

        internal static Results<NoContent, ForbidHttpResult> GetAllClients()
        {
            throw new NotImplementedException();
        }

        internal static async Task<Results<Ok<CreateClientResult>, IResult>> CreateClient([FromBody] CreateClientRequest request,
        [FromServices] IExecutor<ICreateClient, CreateClientRequest, Result<CreateClientResult>> createClient,
        CancellationToken cancellationToken)
        {
            var result = await createClient.Execute(request, cancellationToken);
            return result.MapToHttpResult(TypedResults.Ok);
        }
    }
}
