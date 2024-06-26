﻿using InDuckTor.Shared.Security.Context;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Employee.GetAllEmployees
{
    public class GetAllEmployees : IGetAllEmployees
    {
        private readonly UsersDbContext _context;

        public GetAllEmployees(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ShortEmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var searchParams = request.SearchParams;

            var employees = _context.Users.Include(u => u.Employee).Where(u => u.Employee != null).Include(u => u.Bans).AsQueryable();

            switch (searchParams.Status)
            {
                case (EmployeeStatus.Active):
                    employees = employees.Where(e => e.InactiveAt == null);
                    break;
                case (EmployeeStatus.Inactive):
                    employees = employees.Where(e => e.InactiveAt != null);
                    break;
            }

            return (await employees.ToListAsync(cancellationToken)).Adapt<List<ShortEmployeeDto>>();
        }
    }
}
