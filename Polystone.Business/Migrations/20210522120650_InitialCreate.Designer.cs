﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Polystone.Business;

namespace Polystone.Business.Migrations
{
    [DbContext(typeof(PolystoneContext))]
    [Migration("20210522120650_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("Polystone.Business.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CaughtPokemons")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EscapedPokemons")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HashedName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<int>("Pokestops")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Raids")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rockets")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ShinyPokemons")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalStardust")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TotalXp")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
