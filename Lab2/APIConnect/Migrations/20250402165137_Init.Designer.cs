﻿// <auto-generated />
using APIConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APIConnect.Migrations
{
    [DbContext(typeof(CurrencyExchangeOffice))]
    [Migration("20250402165137_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("APIConnect.BaseCurrency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("timestamp")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("BaseCurrencies");
                });

            modelBuilder.Entity("APIConnect.CurrencyRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaseCurrencyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExchangeCurrency")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("ExchangeRate")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BaseCurrencyId");

                    b.ToTable("CurrenciesRates");
                });

            modelBuilder.Entity("APIConnect.CurrencyRate", b =>
                {
                    b.HasOne("APIConnect.BaseCurrency", null)
                        .WithMany("ExchangeCurrencies")
                        .HasForeignKey("BaseCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("APIConnect.BaseCurrency", b =>
                {
                    b.Navigation("ExchangeCurrencies");
                });
#pragma warning restore 612, 618
        }
    }
}
