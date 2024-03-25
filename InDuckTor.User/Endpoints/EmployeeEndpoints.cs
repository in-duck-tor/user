using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Features.Employee.CreateEmployee;
using InDuckTor.User.Features.Employee.GetAllEmployees;
using InDuckTor.User.Features.Permission.GetAllPermissions;
using InDuckTor.User.WebApi.Validation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;


namespace InDuckTor.User.WebApi.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static IEndpointRouteBuilder AddEmployeeEndpoints(this IEndpointRouteBuilder builder)
        {
            var groupBuilder = builder.MapGroup("/api/v1")
                .WithTags("Employee")
                .WithOpenApi()
                .RequireAuthorization();

            groupBuilder.MapGet("/employee", GetAllEmployees)
                .WithDescription("Получить краткую информацю о всех сотрудниках");

            groupBuilder.MapPost("/employee", CreateEmployee)
                .WithDescription("Создать сотрудника")
                .AddEndpointFilter<ValidationFilter<CreateEmployeeRequest>>();

            groupBuilder.MapGet("/employee/permissions", GetEmployeePermissions)
                .WithDescription("Получить все доступные права для сотрудников");

            return builder;
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType<IEnumerable<ShortEmployeeDto>>(200)]
        internal static async Task<IResult> GetAllEmployees([FromQuery] EmployeeStatus? status,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllEmployeesQuery(new EmployeesSearchParams(status)), cancellationToken);
            return Results.Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType(400)]
        [ProducesResponseType<CreateEmployeeResult>(200)]
        internal static async Task<IResult> CreateEmployee([FromBody] CreateEmployeeRequest request,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateEmployeeCommand(request), cancellationToken);
            return Results.Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [ProducesResponseType(403)]
        [ProducesResponseType<IEnumerable<PermissionDto>>(200)]
        internal static async Task<IResult> GetEmployeePermissions([FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllPermissionsQuery(Role.Employee), cancellationToken);
            return Results.Ok(result);   
        }
    }
}
