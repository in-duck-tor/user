using MediatR;

namespace InDuckTor.User.Features.BlackList.DeleteBan
{
    public record DeleteBanCommand(long UserId) : IRequest;

    public interface IDeleteBan : IRequestHandler<DeleteBanCommand>;
}
