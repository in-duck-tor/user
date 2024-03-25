using InDuckTor.User.Infrastructure.Database;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.Permission.GetAllPermissions
{
    public class GetAllPermissions : IGetAllPermissions
    {
        private readonly UsersDbContext _context;

        public GetAllPermissions(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        => (await _context.Permissions.Where(p => p.Role == request.Role).ToListAsync(cancellationToken)).Adapt<List<PermissionDto>>();
    }
}
