using MediatR;

namespace InDuckTor.User.Features.Client.GetAllClients
{
    public enum ClientStatus
    {
        Active,
        Inactive,
    }
    public record ClientsSearchParams(ClientStatus? Status);

    public record ShortClientDto(long Id, string Login, string? Email, string FirstName, string LastName, string? MiddleName, DateTime? InactiveAt, Boolean IsBlocked);

    public record GetAllClientsQuery(ClientsSearchParams SearchParams) : IRequest<IEnumerable<ShortClientDto>>;

    public interface IGetAllClients : IRequestHandler<GetAllClientsQuery, IEnumerable<ShortClientDto>>;
 
}
