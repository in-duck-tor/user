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

            var clients = _context.Users.Include(u => u.Client).Where(u => u.Client != null).Include(u => u.Bans).AsQueryable();

            switch (searchParams.Status)
            {
                case (ClientStatus.Active):
                    clients = clients.Where(e => e.InactiveAt == null);
                    break;
                case (ClientStatus.Inactive):
                    clients = clients.Where(e => e.InactiveAt != null);
                    break;
            }

            return (await clients.ToListAsync(cancellationToken)).Adapt<List<ShortClientDto>>();
        }
    }
}
