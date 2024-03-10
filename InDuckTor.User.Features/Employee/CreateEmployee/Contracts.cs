using FluentValidation;
using MediatR;

namespace InDuckTor.User.Features.Employee.CreateEmployee
{
    public record CreateEmployeeRequest(string Login, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? BirthDate, List<string> Position, List<string> Permissions);
    public record CreateEmployeeResult(long Id);
    public record CreateEmployeeCommand(CreateEmployeeRequest EmployeeRequest) : IRequest<CreateEmployeeResult>;

    public interface ICreateEmployee : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResult>;

    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator() {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Login).NotEmpty();
            RuleFor(r => r.Position).NotNull();
            RuleFor(r => r.Permissions).NotNull();
        }
    }
}
