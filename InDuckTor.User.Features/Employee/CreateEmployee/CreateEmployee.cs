using FluentResults;
using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Domain;
using InDuckTor.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace InDuckTor.User.Features.Employee.CreateEmployee
{
    public class CreateEmployee : ICreateEmployee
    {
        private readonly UsersDbContext _context;
        private readonly ISecurityContext _securityContext;

        public CreateEmployee(UsersDbContext context, ISecurityContext securityContext)
        {
            _context = context;
            _securityContext = securityContext;
        }

        public async Task<Result<CreateEmployeeResult>> Execute(CreateEmployeeRequest input, CancellationToken ct)
        {
            var user = await _context.Users.Where(u => u.Login == input.Login).FirstOrDefaultAsync(ct);
            if (user is not null) return new Errors.User.LoginExists(input.Login);

            var permissions = new List<Permission>();

            foreach (var permissionKey in input.Permissions)
            {
                var permission = await _context.Permissions.Where(p => p.Key == permissionKey).FirstOrDefaultAsync(ct);
                if (permission is null) return new Errors.Permission.NotFound(permissionKey);
                permissions.Add(permission);
            }

            var employee = new Domain.Employee
            {

                User = new Domain.User
                {
                    Login = input.Login,
                    AccountType = Domain.AccountType.Client,
                },
                Email = input.Email,
                FirstName = input.FirstName,
                LastName = input.LastName,
                MiddleName = input.MiddleName,
                BirthDate = input.BirthDate,
                Position  = input.Position,
                Permissions = permissions,
            };

            _context.Add(employee);
            await _context.SaveChangesAsync(ct);

            return new CreateEmployeeResult(employee.Id);
        }
    }
}
