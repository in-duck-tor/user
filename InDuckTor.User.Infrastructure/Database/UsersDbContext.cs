using InDuckTor.User.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace InDuckTor.User.Infrastructure.Database
{
    public class UsersDbContext : DbContext
    {

        public string? Schema { get; }

        public UsersDbContext(DbContextOptions<UsersDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Schema = dbContextOptions.Extensions.OfType<UsersDbContextOptionsExtension>()
                .FirstOrDefault()
                ?.Schema;
        }

        public virtual DbSet<Domain.BaseUser> Users { get; } = null!;
        public virtual DbSet<Employee> Employees { get; } = null!;
        public virtual DbSet<Client> Clients { get; } = null!;
        public virtual DbSet<Permission> Permissions { get; } = null!;
        public virtual DbSet<BlackList> BlackList { get; } = null!;

        public const string UserPersonalCodeSequenceName = "user_personal_code_seq";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasKey(p => p.Key);

            modelBuilder
                .Entity<BaseUser>()
                .Property(u => u.AccountType)
                .HasConversion(new EnumToStringConverter<AccountType>());

            modelBuilder.Entity<BaseUser>().UseTpcMappingStrategy();

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasSequence<long>(UserPersonalCodeSequenceName);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp without time zone");
        }
    }
}
