using FluentResults;
using InDuckTor.Shared.Strategies;

namespace InDuckTor.User.Features.Client.CreateClient
{
    public record CreateClientRequest(string Login, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? BirthDate);
    public record CreateClientResult(long Id);

    public interface ICreateClient : ICommand<CreateClientRequest, Result<CreateClientResult>>;
}
