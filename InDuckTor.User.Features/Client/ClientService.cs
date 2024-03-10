

using FluentResults;
using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Infrastructure.Database;

namespace InDuckTor.User.Features.Client
{
    public class ClientService : IClientService
    {
        private readonly UsersDbContext _context;
        private readonly ISecurityContext _securityContext;

        public ClientService(UsersDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<Result<CreateClientResult>> Create(CreateClientRequest request)
        {
            var user = await _context.Users.FindAsync(request.Login);

            if (user is not null) return new Errors.User.LoginExists(request.Login);

            var client = new Domain.Client
            {

                Login = request.Login,
                AccountType = Domain.AccountType.Client,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                BirthDate = request.BirthDate
            };

            _context.Add(client);
            await _context.SaveChangesAsync();

            return new CreateClientResult(client.Id);
        }
    }
}
