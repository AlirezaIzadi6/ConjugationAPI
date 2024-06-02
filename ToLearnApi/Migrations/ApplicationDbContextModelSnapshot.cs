﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToLearnApi.Contexts;

#nullable disable

namespace ToLearnApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToLearnApi.Models.Conjugation.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("answers", "conjugation");
                });

            modelBuilder.Entity("ToLearnApi.Models.Conjugation.Conjugation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Form1P")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form1S")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form2P")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form2S")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form3P")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Form3S")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gerund")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GerundEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Infinitive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InfinitiveEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoodEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PastParticiple")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PastParticipleEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tense")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenseEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerbEnglish")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("conjugations", "conjugation");
                });

            modelBuilder.Entity("ToLearnApi.Models.Conjugation.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Infinitives")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Moods")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Persons")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("profiles", "conjugation");
                });

            modelBuilder.Entity("ToLearnApi.Models.Conjugation.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasBeenAnswered")
                        .HasColumnType("bit");

                    b.Property<string>("Infinitive")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mood")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Person")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("questions", "conjugation");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("cards", "flashcards");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Deck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("decks", "flashcards");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.LearnAndReview.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CardId")
                        .HasColumnType("int");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastReview")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Learned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LearnedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextReview")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfReviews")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.ToTable("items", "flashcards");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.LearnAndReview.LearnStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInitialized")
                        .HasColumnType("bit");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("learnStatuses", "flashcards");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Creator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeckId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("units", "flashcards");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("roles", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("roleClaims", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("users", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("userClaims", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("userLogins", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("userRoles", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserToken", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("userTokens", "security");
                });

            modelBuilder.Entity("ToLearnApi.Models.Conjugation.Answer", b =>
                {
                    b.HasOne("ToLearnApi.Models.Conjugation.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Card", b =>
                {
                    b.HasOne("ToLearnApi.Models.Flashcards.Unit", "Unit")
                        .WithMany("Cards")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.LearnAndReview.Item", b =>
                {
                    b.HasOne("ToLearnApi.Models.Flashcards.Card", "Card")
                        .WithMany("Items")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Unit", b =>
                {
                    b.HasOne("ToLearnApi.Models.Flashcards.Deck", "Deck")
                        .WithMany("Units")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomRoleClaim", b =>
                {
                    b.HasOne("ToLearnApi.Models.Identity.CustomRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserClaim", b =>
                {
                    b.HasOne("ToLearnApi.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserLogin", b =>
                {
                    b.HasOne("ToLearnApi.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserRole", b =>
                {
                    b.HasOne("ToLearnApi.Models.Identity.CustomRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToLearnApi.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToLearnApi.Models.Identity.CustomUserToken", b =>
                {
                    b.HasOne("ToLearnApi.Models.Identity.CustomUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Card", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Deck", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("ToLearnApi.Models.Flashcards.Unit", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
