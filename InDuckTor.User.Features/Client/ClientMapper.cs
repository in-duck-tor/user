using InDuckTor.User.Domain;
using InDuckTor.User.Domain.Specifications;
using InDuckTor.User.Features.Client.CreateClient;
using InDuckTor.User.Features.Client.GetAllClients;
using Mapster;

namespace InDuckTor.User.Features.Client
{
    public static class ClientMapper
    {
        public static void RegisterMapsterConfiguration()
        {
            TypeAdapterConfig<CreateClientRequest, Domain.Client>
               .NewConfig()
               .Map(dest => dest.User, src => new Domain.User { Login = src.Login, AccountType = AccountType.Client });

            TypeAdapterConfig<Domain.Client, ShortClientDto>
               .NewConfig()
               .Map(dest => dest.Login, src => src.User.Login)
               .Map(dest => dest.IsBlocked, src => src.User.Bans.SingleOrDefault((Specifications.BlackList.ActiveBanAsFunc(src.Id))) != null); ;
        }
    }
}
