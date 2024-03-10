using Microsoft.AspNetCore.Http.HttpResults;

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
                .WithDescription("Создать сотрудника");

            groupBuilder.MapGet("/employee/permissions", GetEmployeePermissions)
                .WithDescription("Получить все доступные права для сотрудников");

            return builder;
        }

        internal static Results<NoContent, ForbidHttpResult> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        internal static Results<NoContent, ForbidHttpResult> CreateEmployee()
        {
            throw new NotImplementedException();
        }

        internal static Results<NoContent, ForbidHttpResult> GetEmployeePermissions()
        {
            throw new NotImplementedException();
        }
    }
}
