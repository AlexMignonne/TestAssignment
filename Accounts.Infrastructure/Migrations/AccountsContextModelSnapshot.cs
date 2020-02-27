﻿// <auto-generated />

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Accounts.Infrastructure.Migrations
{
    [DbContext(typeof(AccountsContext))]
    internal class AccountsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(
            ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation(
                    "Npgsql:ValueGenerationStrategy",
                    NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation(
                    "ProductVersion",
                    "3.1.2")
                .HasAnnotation(
                    "Relational:MaxIdentifierLength",
                    63);

            modelBuilder.Entity(
                "Accounts.Domain.AggregatesModel.Account.AccountDomain",
                b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation(
                            "Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccountStatus")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProvinceId")
                        .HasColumnType("integer");

                    b.HasKey("Id");
                    b.ToTable("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
