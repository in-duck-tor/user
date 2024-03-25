using InDuckTor.User.Domain;
using InDuckTor.User.Features.HttpClients;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Client.CreateClient
{
    public class CreateClient : ICreateClient
    {
        private readonly UsersDbContext _context;
        private readonly IAuthHttpClient _authHttpClient;

        public CreateClient(UsersDbContext context, IAuthHttpClient authHttpClient)
        {
            _context = context;
            _authHttpClient = authHttpClient;
        }

        public async Task<CreateClientResult> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var req = request.ClientRequest;

            var response = await _authHttpClient.RegisterCredentials(new RegisterCredentialsRequest(req.Login, req.Password));

            if (!response.Succeed) throw new Errors.User.LoginExists(req.Login);

            var user = req.Adapt<Domain.User>();
            user.Id = response.Content;
            user.Client!.Id = user.Id;

            _context.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateClientResult(user.Id);
        }
    }
}
