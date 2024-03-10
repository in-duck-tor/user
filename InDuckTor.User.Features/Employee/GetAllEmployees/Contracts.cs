using MediatR;

namespace InDuckTor.User.Features.Employee.GetAllEmployees
{
    public enum EmployeeStatus
    {
        Active,
        Inactive,
    }
    public record EmployeesSearchParams(EmployeeStatus? Status);

    public record ShortEmployeeDto(long Id, string Login, string? Email, string FirstName, string LastName, string? MiddleName, List<string> Position, DateTime? InactiveAt, Boolean IsBlocked);

    public record GetAllEmployeesQuery(EmployeesSearchParams SearchParams) : IRequest<IEnumerable<ShortEmployeeDto>>;

    public interface IGetAllEmployees : IRequestHandler<GetAllEmployeesQuery, IEnumerable<ShortEmployeeDto>>;
}
