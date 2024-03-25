using InDuckTor.User.Domain;
using MediatR;

namespace InDuckTor.User.Features.Permission.GetAllPermissions
{
    public record PermissionDto(string Key, string Description);

    public record GetAllPermissionsQuery(Role Role) : IRequest<IEnumerable<PermissionDto>>;

    public interface IGetAllPermissions : IRequestHandler<GetAllPermissionsQuery, IEnumerable<PermissionDto>>;
}
