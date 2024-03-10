using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InDuckTor.User.Infrastructure.Database
{
    public record DatabaseSettings(string Scheme);

    public static class DependencyRegistration
    {
        public static IServiceCollection AddUsersDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var configurationSection = configuration.GetSection(nameof(DatabaseSettings));
            serviceCollection.Configure<DatabaseSettings>(configurationSection);
            var databaseSettings = configurationSection.Get<DatabaseSettings>();
            ArgumentNullException.ThrowIfNull(databaseSettings, nameof(configuration));

            return serviceCollection.AddDbContext<UsersDbContext>(optionsBuilder =>
            {
                ((IDbContextOptionsBuilderInfrastructure)optionsBuilder)
                    .AddOrUpdateExtension(new UsersDbContextOptionsExtension(databaseSettings.Scheme));

                optionsBuilder
                    .UseNpgsql(configuration.GetConnectionString("UserDatabase"));
            });
        }
    }

    internal class UsersDbContextOptionsExtension : IDbContextOptionsExtension
    {
        public string? Schema { get; private set; }
        private DbContextOptionsExtensionInfo? _info;

        public UsersDbContextOptionsExtension(string? schema = null)
        {
            Schema = schema;
        }

        public void ApplyServices(IServiceCollection services)
        {
        }

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info => _info ??= new UsersDbContextOptionsExtensionInfo(this);
    }

    internal class UsersDbContextOptionsExtensionInfo : DbContextOptionsExtensionInfo
    {
        public UsersDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
        }

        public override int GetServiceProviderHashCode() => 0;

        public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other) => other is UsersDbContextOptionsExtensionInfo;

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
        }

        public override bool IsDatabaseProvider => false;
        public override string LogFragment => string.Empty;
    }
}
