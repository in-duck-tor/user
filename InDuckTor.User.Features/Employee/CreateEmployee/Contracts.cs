using FluentResults;
using InDuckTor.Shared.Strategies;

namespace InDuckTor.User.Features.Employee.CreateEmployee
{
    public record CreateEmployeeRequest(string Login, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? BirthDate, List<string> Position, List<string> Permissions);
    public record CreateEmployeeResult(long Id);

    public interface ICreateEmployee : ICommand<CreateEmployeeRequest, Result<CreateEmployeeResult>>;

}
