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
    [Migration("20240310100013_Initial")]
    partial class Initial
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

            modelBuilder.HasSequence("BaseUserSequence");

            modelBuilder.HasSequence("user_personal_code_seq");

            modelBuilder.Entity("EmployeePermission", b =>
                {
                    b.Property<long>("EmployeesId")
                        .HasColumnType("bigint");

                    b.Property<string>("PermissionsKey")
                        .HasColumnType("text");

                    b.HasKey("EmployeesId", "PermissionsKey");

                    b.HasIndex("PermissionsKey");

                    b.ToTable("EmployeePermission", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.BaseUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasDefaultValueSql("nextval('\"user\".\"BaseUserSequence\"')");

                    NpgsqlPropertyBuilderExtensions.UseSequence(b.Property<long>("Id"));

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("InactiveAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("InDuckTor.User.Domain.BlackList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("BlackList", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Permission", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Key");

                    b.ToTable("Permissions", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Client", b =>
                {
                    b.HasBaseType("InDuckTor.User.Domain.BaseUser");

                    b.ToTable("Clients", "user");
                });

            modelBuilder.Entity("InDuckTor.User.Domain.Employee", b =>
                {
                    b.HasBaseType("InDuckTor.User.Domain.BaseUser");

                    b.Property<List<string>>("Position")
                        .HasColumnType("text[]");

                    b.ToTable("Employees", "user");
                });

            modelBuilder.Entity("EmployeePermission", b =>
                {
                    b.HasOne("InDuckTor.User.Domain.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InDuckTor.User.Domain.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
