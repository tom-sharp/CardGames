﻿// <auto-generated />
using Games.Card.TexasHoldEm;
using Games.Card.TexasHoldEm.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(TexasDbContext))]
    [Migration("20230111115203_intit2")]
    partial class intit2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Games.Card.TexasHoldEm.TexasStatisticsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("CommonCard1")
                        .HasColumnType("tinyint");

                    b.Property<byte>("CommonCard2")
                        .HasColumnType("tinyint");

                    b.Property<byte>("CommonCard3")
                        .HasColumnType("tinyint");

                    b.Property<byte>("CommonCard4")
                        .HasColumnType("tinyint");

                    b.Property<byte>("CommonCard5")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Players")
                        .HasColumnType("tinyint");

                    b.Property<byte>("PrivateCard1")
                        .HasColumnType("tinyint");

                    b.Property<byte>("PrivateCard2")
                        .HasColumnType("tinyint");

                    b.Property<byte>("RankId")
                        .HasColumnType("tinyint");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TexasHands");
                });
#pragma warning restore 612, 618
        }
    }
}
