using InDuckTor.User.Features.HttpClients;
using Microsoft.Extensions.Options;

namespace InDuckTor.User.WebApi.Configuration;

public static class HttpClientsConfiguration
{
    internal const string AuthServiceConfiguration = nameof(AuthServiceConfiguration);

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HttpClientConfiguration>(name: AuthServiceConfiguration, configuration.GetSection(AuthServiceConfiguration));
        services.AddHttpClient<IAuthHttpClient, AuthHttpClient>(static (provider, client) =>
        {
            var options = provider.GetRequiredService<IOptionsMonitor<HttpClientConfiguration>>();
            var httpClientConfiguration = options.Get(AuthServiceConfiguration);
            client.BaseAddress = httpClientConfiguration.BaseUrl;
        });

        return services;
    }
}

internal class HttpClientConfiguration
{
    public Uri BaseUrl { get; set; }
}