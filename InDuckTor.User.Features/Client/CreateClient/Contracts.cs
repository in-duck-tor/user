using FluentValidation;
using MediatR;

namespace InDuckTor.User.Features.Client.CreateClient
{
    public record CreateClientRequest(string Login, string Password, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? BirthDate);
    public record CreateClientResult(long Id);
    public record CreateClientCommand(CreateClientRequest ClientRequest) : IRequest<CreateClientResult>;

    public interface ICreateClient : IRequestHandler<CreateClientCommand, CreateClientResult>;

    public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
    {
        public CreateClientRequestValidator()
        {
            RuleFor(r => r.FirstName).NotEmpty();
            RuleFor(r => r.LastName).NotEmpty();
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Login).NotEmpty();
            RuleFor(r => r.Password).MinimumLength(6);
        }
    }
}
