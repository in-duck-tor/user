using InDuckTor.User.Domain;
using MediatR;

namespace InDuckTor.User.Features.User.GetUserClaims
{
    public class UserClaimsDto
    {
        public string Login {  get; set; }
        public List<string> Permissions { get; set; } = new();
        public List<Role> Roles { get; set; } = new();
        public bool IsActive { get; set; }
    }

    public record GetUserClaimsQuery(long Id) : IRequest<UserClaimsDto>;

    public interface IGetUserClaims : IRequestHandler<GetUserClaimsQuery, UserClaimsDto>;
}
