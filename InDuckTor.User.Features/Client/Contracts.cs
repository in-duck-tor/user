using FluentResults;
using InDuckTor.Shared.Strategies;

namespace InDuckTor.User.Features.Client
{
    public record CreateClientRequest(string Login, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? BirthDate);
    public record CreateClientResult(long Id);

    public interface IClientService
    {
        Task<Result<CreateClientResult>> Create(CreateClientRequest request);
    }
}
