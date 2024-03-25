using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Features.HttpClients;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Employee.CreateEmployee
{
    public class CreateEmployee : ICreateEmployee
    {
        private readonly UsersDbContext _context;
        private readonly IAuthHttpClient _authHttpClient;

        public CreateEmployee(UsersDbContext context, IAuthHttpClient authHttpClient)
        {
            _context = context;
            _authHttpClient = authHttpClient;
        }

        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var req = request.EmployeeRequest;

            var permissions = new List<Domain.Permission>();
            foreach (var permissionKey in req.Permissions)
            {
                var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Key == permissionKey, cancellationToken) ??
                    throw new Errors.Permission.NotFound(permissionKey);

                permissions.Add(permission);
            }

            var response = await _authHttpClient.RegisterCredentials(new RegisterCredentialsRequest(req.Login, req.Password));

            if (!response.Succeed) throw new Errors.User.LoginExists(req.Login);

            var user = req.Adapt<Domain.User>();
            user.Employee!.Permissions = permissions;
            user.Id = response.Content;

            _context.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateEmployeeResult(user.Id);
        }
    }
}
