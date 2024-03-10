using InDuckTor.User.Domain;
using InDuckTor.User.Domain.Specifications;
using InDuckTor.User.Features.Employee.CreateEmployee;
using InDuckTor.User.Features.Employee.GetAllEmployees;
using Mapster;
using System.Linq;

namespace InDuckTor.User.Features.Employee
{
    public static class EmployeeMapper
    {
        public static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<CreateEmployeeRequest, Domain.Employee>
               .NewConfig()
               .Map(dest => dest.User, src => new Domain.User { Login = src.Login, AccountType = AccountType.Employee })
               .Ignore(dest => dest.Permissions);

            TypeAdapterConfig<Domain.Employee, ShortEmployeeDto>
               .NewConfig()
               .Map(dest => dest.Login, src => src.User.Login)
               .Map(dest => dest.InactiveAt, src => src.User.InactiveAt)
               .Map(dest => dest.IsBlocked, src => src.User.Bans.SingleOrDefault((Specifications.BlackList.ActiveBanAsFunc(src.Id))) != null);
        }

    }
}
