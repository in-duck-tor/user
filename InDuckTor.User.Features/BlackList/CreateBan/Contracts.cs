using FluentValidation;
using MediatR;

namespace InDuckTor.User.Features.BlackList.CreateBan
{
    public record CreateBanRequest(long UserId, DateTime? EndDate, string? Reason);
    public record CreateBanCommand(CreateBanRequest BanRequest) : IRequest;

    public interface ICreateBan : IRequestHandler<CreateBanCommand>;
}
