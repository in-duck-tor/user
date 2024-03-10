using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Features.Client.GetAllClients;
using InDuckTor.User.Features.Employee.GetAllEmployees;
using InDuckTor.User.WebApi.Validation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
                .WithDescription("Создать клиента")
                .AddEndpointFilter<ValidationFilter<CreateClientRequest>>(); 

            return builder;
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType<IEnumerable<ShortClientDto>>(200)]
        internal static async Task<IResult> GetAllClients([FromQuery] ClientStatus? status,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllClientsQuery(new ClientsSearchParams(status)), cancellationToken);
            return Results.Ok(result);
        }


        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(400)]
        [ProducesResponseType<CreateClientResult>(200)]
        internal static async Task<IResult> CreateClient([FromBody] CreateClientRequest request,
        [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateClientCommand(request), cancellationToken);
            return Results.Ok(result);
        }
    }
}
