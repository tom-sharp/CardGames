﻿// <auto-generated />
using Games.Card.TexasHoldEm.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Games.Card.TexasHoldEm.Data;

namespace Data.Migrations
{
    [DbContext(typeof(TexasDbContext))]
    [Migration("20230121210758_AddAiDb2")]
    partial class AddAiDb2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasHoldEmAiEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("PCount")
                        .HasColumnType("int");

                    b.Property<int>("WCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TexasAI");
                });

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasPlayRoundEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Card1Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card2Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card3RankId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card3Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card4RankId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card4Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card5RankId")
                        .HasColumnType("tinyint");

                    b.Property<string>("Card5RankName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte>("Card5Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Players")
                        .HasColumnType("tinyint");

                    b.Property<byte>("WinRankId")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.ToTable("PlayRounds");
                });

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasPlayerHandEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Card1Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card2RankId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card2Signature")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card5RankId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Card6RankId")
                        .HasColumnType("tinyint");

                    b.Property<byte>("HandRankId")
                        .HasColumnType("tinyint");

                    b.Property<string>("HandRankName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PlayRoundId")
                        .HasColumnType("int");

                    b.Property<bool>("WinRound")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PlayRoundId");

                    b.ToTable("PlayRoundHands");
                });

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasStatisticsEntity", b =>
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

                    b.Property<byte>("RankIdCommonCards")
                        .HasColumnType("tinyint");

                    b.Property<byte>("RankIdPrivateCards")
                        .HasColumnType("tinyint");

                    b.Property<string>("RankName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RankNameCommon")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RankNamePrivate")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("Win")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("TexasHands");
                });

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasPlayerHandEntity", b =>
                {
                    b.HasOne("Games.Card.TexasHoldEm.Models.TexasPlayRoundEntity", "PlayRound")
                        .WithMany("PlayerHands")
                        .HasForeignKey("PlayRoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayRound");
                });

            modelBuilder.Entity("Games.Card.TexasHoldEm.Models.TexasPlayRoundEntity", b =>
                {
                    b.Navigation("PlayerHands");
                });
#pragma warning restore 612, 618
        }
    }
}
