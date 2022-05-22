﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(RoomWarsContext))]
    partial class RoomWarsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.GameRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CurrentTotalUser")
                        .HasColumnType("integer");

                    b.Property<Guid>("HostId")
                        .HasColumnType("uuid");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoomStatus")
                        .HasColumnType("integer");

                    b.Property<int>("TotalUser")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HostId");

                    b.ToTable("GameRoom");
                });

            modelBuilder.Entity("Domain.Entities.GameStatistic", b =>
                {
                    b.Property<Guid>("GameRoomId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LoserId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoundCount")
                        .HasColumnType("integer");

                    b.Property<int>("WinnerHealth")
                        .HasColumnType("integer");

                    b.Property<Guid>("WinnerId")
                        .HasColumnType("uuid");

                    b.HasKey("GameRoomId");

                    b.HasIndex("LoserId");

                    b.HasIndex("WinnerId");

                    b.ToTable("GameStatistic");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameRoomUser", b =>
                {
                    b.Property<Guid>("GameRoomsParticipationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("GameRoomsParticipationId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("GameRoomUser");
                });

            modelBuilder.Entity("Domain.Entities.GameRoom", b =>
                {
                    b.HasOne("Domain.Entities.User", "Host")
                        .WithMany("GameRooms")
                        .HasForeignKey("HostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Host");
                });

            modelBuilder.Entity("Domain.Entities.GameStatistic", b =>
                {
                    b.HasOne("Domain.Entities.GameRoom", "GameRoom")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Entities.GameStatistic", "GameRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Loser")
                        .WithMany("LoseGames")
                        .HasForeignKey("LoserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Winner")
                        .WithMany("WinGames")
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRoom");

                    b.Navigation("Loser");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("GameRoomUser", b =>
                {
                    b.HasOne("Domain.Entities.GameRoom", null)
                        .WithMany()
                        .HasForeignKey("GameRoomsParticipationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.GameRoom", b =>
                {
                    b.Navigation("Statistic")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("GameRooms");

                    b.Navigation("LoseGames");

                    b.Navigation("WinGames");
                });
#pragma warning restore 612, 618
        }
    }
}
