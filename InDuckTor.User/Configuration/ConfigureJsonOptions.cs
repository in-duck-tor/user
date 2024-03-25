using InDuckTor.User.Domain;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace InDuckTor.User.WebApi.Configuration
{
    public static class JsonConfigurationExtensions
    {
        public static IServiceCollection ConfigureJsonConverters(this IServiceCollection serviceCollection) => serviceCollection.Configure<JsonOptions>(ConfigureJsonOptions);

        private static void ConfigureJsonOptions(JsonOptions options)
        {
            var enumMemberConverter = new JsonStringEnumMemberConverter(
                new JsonStringEnumMemberConverterOptions(),
                typeof(Role));

            options.SerializerOptions.Converters.Add(enumMemberConverter);
        }
  
    }
}
