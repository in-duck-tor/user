using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Features.Employee.GetAllEmployees;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Client.GetAllClients
{
    public class GetAllClients : IGetAllClients
    {
        private readonly UsersDbContext _context;

        public GetAllClients(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShortClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var searchParams = request.SearchParams;

            var clients = _context.Clients.Include(e => e.User).ThenInclude(u => u.Bans).AsQueryable();

            switch (searchParams.Status)
            {
                case (ClientStatus.Active):
                    clients = clients.Where(e => e.User.InactiveAt == null);
                    break;
                case (ClientStatus.Inactive):
                    clients = clients.Where(e => e.User.InactiveAt != null);
                    break;
            }

            return (await clients.ToListAsync(cancellationToken)).Adapt<List<ShortClientDto>>();
        }
    }
}
