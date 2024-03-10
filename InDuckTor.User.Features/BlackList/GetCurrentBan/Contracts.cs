using MediatR;

namespace InDuckTor.User.Features.BlackList.GetCurrentBan
{
    public record GetCurrentBanQuery(long UserId) : IRequest<Domain.BlackList?>;

    public interface IGetCurrentBan : IRequestHandler<GetCurrentBanQuery, Domain.BlackList?>;
}
