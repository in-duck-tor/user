using InDuckTor.User.Domain;
using InDuckTor.User.Features.BlackList.GetCurrentBan;
using InDuckTor.User.Infrastructure.Database;
using MediatR;

namespace InDuckTor.User.Features.BlackList.DeleteBan
{
    public class DeleteBan : IDeleteBan
    {
        private readonly UsersDbContext _context;
        private readonly IMediator _mediator;

        public DeleteBan(UsersDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(DeleteBanCommand request, CancellationToken cancellationToken)
        {
            var existedBan = await _mediator.Send(new GetCurrentBanQuery(request.UserId));
            if (existedBan is null) throw new Errors.BlackList.NotFound(request.UserId);

            _context.Remove(existedBan);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
