using InDuckTor.User.Features.Employee.CreateEmployee;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Features.BlackList.CreateBan;

namespace InDuckTor.User.Features
{
    public static class ValidatorConfiguration
    {
        public static void RegisterValidatorConfiguration(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining(typeof(CreateEmployeeRequestValidator));
            services.AddValidatorsFromAssemblyContaining(typeof(CreateClientRequestValidator));
        }
    }
}
