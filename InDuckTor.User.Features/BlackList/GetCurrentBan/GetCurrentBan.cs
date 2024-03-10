using InDuckTor.User.Domain;
using InDuckTor.User.Domain.Specifications;
using InDuckTor.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace InDuckTor.User.Features.BlackList.GetCurrentBan
{
    public class GetCurrentBan : IGetCurrentBan
    {
        private readonly UsersDbContext _context;

        public GetCurrentBan(UsersDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.BlackList?> Handle(GetCurrentBanQuery request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
            if (user is null) throw new Errors.User.NotFound(userId);

            var existedBan = await _context.BlackList.Where(Specifications.BlackList.ActiveBan(user.Id)).FirstOrDefaultAsync(cancellationToken);

            return existedBan;
        }
    }
}
