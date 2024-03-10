using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InDuckTor.User.WebApi.Endpoints
{
    public static class ClientEndpoints
    {
        public static IEndpointRouteBuilder AddClientEndpoints(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("/api/v1")
                .WithTags("Clients")
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

        internal static Results<NoContent, ForbidHttpResult> CreateClient()
        {
            throw new NotImplementedException();
        }
    }
}
