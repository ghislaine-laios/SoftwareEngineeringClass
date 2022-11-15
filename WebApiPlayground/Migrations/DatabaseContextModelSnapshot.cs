﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiPlayground.Model;

#nullable disable

namespace WebApiPlayground.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("QuestioningModule")
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChatSessionUser", b =>
                {
                    b.Property<long>("ChatSessionsId")
                        .HasColumnType("bigint");

                    b.Property<long>("ParticipantsId")
                        .HasColumnType("bigint");

                    b.HasKey("ChatSessionsId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("ChatSessionUser", "QuestioningModule");
                });

            modelBuilder.Entity("WebApiPlayground.Model.ChatSession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("LastId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ChatSessions", "QuestioningModule");
                });

            modelBuilder.Entity("WebApiPlayground.Model.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ChatSessionId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("IdPerChat")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ChatSessionId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages", "QuestioningModule");
                });

            modelBuilder.Entity("WebApiPlayground.Model.Metadata", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Metadata", "QuestioningModule");
                });

            modelBuilder.Entity("WebApiPlayground.Model.Question", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SessionId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SolverId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.HasIndex("SessionId");

                    b.HasIndex("SolverId");

                    b.ToTable("Questions", "QuestioningModule");
                });

            modelBuilder.Entity("WebApiPlayground.Model.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "QuestioningModule");
                });

            modelBuilder.Entity("ChatSessionUser", b =>
                {
                    b.HasOne("WebApiPlayground.Model.ChatSession", null)
                        .WithMany()
                        .HasForeignKey("ChatSessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiPlayground.Model.User", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiPlayground.Model.Message", b =>
                {
                    b.HasOne("WebApiPlayground.Model.ChatSession", null)
                        .WithMany("Messages")
                        .HasForeignKey("ChatSessionId");

                    b.HasOne("WebApiPlayground.Model.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("WebApiPlayground.Model.Question", b =>
                {
                    b.HasOne("WebApiPlayground.Model.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiPlayground.Model.ChatSession", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId");

                    b.HasOne("WebApiPlayground.Model.User", "Solver")
                        .WithMany()
                        .HasForeignKey("SolverId");

                    b.Navigation("Sender");

                    b.Navigation("Session");

                    b.Navigation("Solver");
                });

            modelBuilder.Entity("WebApiPlayground.Model.ChatSession", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
