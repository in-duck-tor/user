using InDuckTor.User.Domain;
using InDuckTor.User.Features.BlackList.GetCurrentBan;
using InDuckTor.User.Infrastructure.Database;
using MediatR;

namespace InDuckTor.User.Features.BlackList.CreateBan
{
    public class CreateBan : ICreateBan
    {
        private readonly UsersDbContext _context;
        private readonly IMediator _mediator;

        public CreateBan(UsersDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // TODO: пользователь не должен банить себя 
        public async Task Handle(CreateBanCommand request, CancellationToken cancellationToken)
        {
            var req = request.BanRequest;

            var existedBan = await _mediator.Send(new GetCurrentBanQuery(req.UserId));
            if (existedBan is not null) throw new Errors.BlackList.BanExists(existedBan.UserId, existedBan.EndAt);

            var ban = new Domain.BlackList
            {
                UserId = req.UserId,
                EndAt = req.EndDate,
                Reason = req.Reason
            };

            _context.Add(ban);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
