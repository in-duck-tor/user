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
            TypeAdapterConfig<CreateClientRequest, Domain.User>
               .NewConfig()
               .Map(dest => dest.Client, src => new Domain.Client());

            TypeAdapterConfig<Domain.User, ShortClientDto>
               .NewConfig()
               .Map(dest => dest.IsBlocked, src => src.Bans.SingleOrDefault((Specifications.BlackList.ActiveBanAsFunc(src.Id))) != null);
        }
    }
}
