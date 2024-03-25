﻿using InDuckTor.User.Domain;
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

        public virtual DbSet<Domain.User> Users { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; } 
        public virtual DbSet<BlackList> BlackList { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasKey(p => p.Key);

            modelBuilder
                .Entity<Permission>()
                .Property(u => u.Role)
                .HasConversion(new EnumToStringConverter<Role>());

            modelBuilder
               .Entity<Domain.User>()
               .HasIndex(u => u.Login)
               .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.User)
                .WithOne(u => u.Employee)
                .HasForeignKey<Employee>(e => e.Id)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.Id)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(Schema);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateTime>().HaveConversion(typeof(DateTimeToDateTimeUtc));
        }
    }

    internal class DateTimeToDateTimeUtc : ValueConverter<DateTime, DateTime>
    {
        public DateTimeToDateTimeUtc() : base(c => DateTime.SpecifyKind(c, DateTimeKind.Utc), c => c)
        {
        }
    }
}
