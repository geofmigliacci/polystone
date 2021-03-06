// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Polystone.Business;

namespace Polystone.Business.Migrations
{
    [DbContext(typeof(PolystoneContext))]
    partial class PolystoneContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Polystone.Business.Models.Account", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("CurrentHistoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastUpdateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrentHistoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountCandy", b =>
                {
                    b.Property<int>("Specie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SmallCandy")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XLCandy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Specie");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountCandy");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountCatch", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong?>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CatchDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Cp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsShadow")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsShiny")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("Specie")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stardust")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountCatch");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountHistory", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("Experience")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<int>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<int>("Pokecoin")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokemonCaught")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PokestopSpinned")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stardust")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("AccountHistory");
                });

            modelBuilder.Entity("Polystone.Business.Models.Account", b =>
                {
                    b.HasOne("Polystone.Business.Models.AccountHistory", "CurrentHistory")
                        .WithMany()
                        .HasForeignKey("CurrentHistoryId");

                    b.Navigation("CurrentHistory");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountCandy", b =>
                {
                    b.HasOne("Polystone.Business.Models.Account", "Account")
                        .WithMany("AccountCandies")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountCatch", b =>
                {
                    b.HasOne("Polystone.Business.Models.Account", "Account")
                        .WithMany("AccountCatches")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Polystone.Business.Models.AccountHistory", b =>
                {
                    b.HasOne("Polystone.Business.Models.Account", "Account")
                        .WithMany("AccountHistories")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Polystone.Business.Models.Account", b =>
                {
                    b.Navigation("AccountCandies");

                    b.Navigation("AccountCatches");

                    b.Navigation("AccountHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
