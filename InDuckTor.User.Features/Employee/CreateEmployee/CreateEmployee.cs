using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Employee.CreateEmployee
{
    public class CreateEmployee : ICreateEmployee
    {
        private readonly UsersDbContext _context;

        public CreateEmployee(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var req = request.EmployeeRequest;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == req.Login, cancellationToken);
            if (user is not null) throw new Errors.User.LoginExists(user.Login);

            var permissions = new List<Domain.Permission>();
            foreach (var permissionKey in req.Permissions)
            {
                var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Key == permissionKey, cancellationToken) ??
                    throw new Errors.Permission.NotFound(permissionKey);

                permissions.Add(permission);
            }

            var employee = req.Adapt<Domain.Employee>();
            employee.Permissions = permissions;

            _context.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateEmployeeResult(employee.Id);
        }
    }
}
