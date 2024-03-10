using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Client.CreateClient
{
    public class CreateClient : ICreateClient
    {
        private readonly UsersDbContext _context;

        public CreateClient(UsersDbContext context, ISecurityContext securityContext)
        {
            _context = context;
        }

        public async Task<CreateClientResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var req = request.ClientRequest;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == req.Login, cancellationToken);
            if (user is not null) throw new Errors.User.LoginExists(req.Login);

            var client = req.Adapt<Domain.Client>();

            _context.Add(client);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateClientResult(client.Id);
        }
    }
}
