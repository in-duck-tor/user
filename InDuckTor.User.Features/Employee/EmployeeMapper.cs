using InDuckTor.User.Domain.Specifications;
using InDuckTor.User.Features.Employee.CreateEmployee;
using InDuckTor.User.Features.Employee.GetAllEmployees;
using Mapster;

namespace InDuckTor.User.Features.Employee
{
    public static class EmployeeMapper
    {
        public static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<CreateEmployeeRequest, Domain.User>
               .NewConfig()
               .Map(dest => dest.Employee, src => new Domain.Employee { Position = src.Position })
               .Map(dest => dest.Client, src => new Domain.Client());

            TypeAdapterConfig<Domain.User, ShortEmployeeDto>
               .NewConfig()
               .Map(dest => dest.Position, src => src.Employee!.Position)
               .Map(dest => dest.IsBlocked, src => src.Bans.SingleOrDefault((Specifications.BlackList.ActiveBanAsFunc(src.Id))) != null);
        }

    }
}
