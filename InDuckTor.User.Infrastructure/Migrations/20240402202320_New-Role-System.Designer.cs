﻿// <auto-generated />
using System;
using System.Collections.Generic;
using InDuckTor.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InDuckTor.User.Infrastructure.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20240402202320_New-Role-System")]
    partial class NewRoleSystem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("user")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InDuckTor.User.Domain.BlackList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("EndAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BlackList", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Client", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Clients", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Employee", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<List<string>>("Position")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("Id");

                    b.ToTable("Employees", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Permission", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Key");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Permissions", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("ClientId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<long?>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("InactiveAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Users", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.BlackList", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.User", null)
                        .WithMany("Bans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Client", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.User", "User")
                        .WithOne()
                        .HasForeignKey("InDuckTor.User.Domain.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Employee", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.User", "User")
                        .WithOne()
                        .HasForeignKey("InDuckTor.User.Domain.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Permission", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.Employee", null)
                        .WithMany("Permissions")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.User", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("InDuckTor.User.Domain.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Client");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Employee", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.User", b =>
                {
                    b.Navigation("Bans");
                });
#pragma warning restore 612, 618
        }
    }
}