using InDuckTor.User.Domain;
using InDuckTor.User.Features.BlackList.GetCurrentBan;
using InDuckTor.User.Infrastructure.Database;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.User.GetUserClaims
{
    public class GetUserClaims : IGetUserClaims
    {
        private readonly UsersDbContext _context;
        private readonly IMediator _mediator;

        public GetUserClaims(UsersDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<UserClaimsDto> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var userId = request.Id;

            var user = await _context.Users.Include(u => u.Employee)
                            .ThenInclude(e => e.Permissions)
                        .Include(u => u.Client)
                        .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null) throw new Errors.User.NotFound(userId);

            var claims = user.Adapt<UserClaimsDto>();
            claims.Roles = new List<Role>();

            if (user.Employee != null)
            {
                claims.Roles.Add(Role.Employee);
                claims.Permissions = user.Employee.Permissions.Select(p => p.Key).ToList();
            }

            if (user.Client != null)
            {
                claims.Roles.Add(Role.Client);
            }

            var ban = await _mediator.Send(new GetCurrentBanQuery(userId));
            claims.IsActive = ban is null && user.InactiveAt is null;

            return claims;
        }
    }
}
