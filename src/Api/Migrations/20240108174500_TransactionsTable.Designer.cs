﻿// <auto-generated />
using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240108174500_TransactionsTable")]
    partial class TransactionsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("personal_finance_management")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Api.Common.Entity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Api.Entities.Account", b =>
                {
                    b.HasBaseType("Api.Common.Entity");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("balance");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("status");

                    b.Property<decimal>("TotalLoan")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_loan");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("accounts", "personal_finance_management");
                });

            modelBuilder.Entity("Api.Entities.Category", b =>
                {
                    b.HasBaseType("Api.Common.Entity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("type");

                    b.HasIndex("Name", "Type")
                        .IsUnique();

                    b.ToTable("categories", "personal_finance_management");
                });

            modelBuilder.Entity("Api.Entities.Transaction", b =>
                {
                    b.HasBaseType("Api.Common.Entity");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("account_id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("amount");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("time");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("type");

                    b.ToTable("transactions", "personal_finance_management");
                });
#pragma warning restore 612, 618
        }
    }
}