using InDuckTor.User.Features.Client;
using InDuckTor.User.Features.Employee;
using Microsoft.Extensions.DependencyInjection;


namespace InDuckTor.User.Features
{
    public static class MapperConfiguration
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            EmployeeMapper.RegisterMapsterConfiguration();
            ClientMapper.RegisterMapsterConfiguration();
        }
    }
}
