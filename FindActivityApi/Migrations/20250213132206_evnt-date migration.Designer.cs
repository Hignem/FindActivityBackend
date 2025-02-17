﻿// <auto-generated />
using System;
using FindActivityApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FindActivityApi.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20250213132206_evnt-date migration")]
    partial class evntdatemigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FindActivityApi.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.HasKey("ActivityId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("FindActivityApi.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FindActivityApi.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EvntId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CommentId");

                    b.HasIndex("EvntId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FindActivityApi.Models.Evnt", b =>
                {
                    b.Property<int>("EvntId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("EvntId"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateOfEvnt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EvntImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("EvntId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Evnts");
                });

            modelBuilder.Entity("FindActivityApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("LatitudeX")
                        .HasColumnType("double precision");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double?>("LongitudeY")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ProfileImagePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FindActivityApi.Models.UserActivity", b =>
                {
                    b.Property<int>("UserActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserActivityId"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserActivityId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActivities");
                });

            modelBuilder.Entity("FindActivityApi.Models.Activity", b =>
                {
                    b.HasOne("FindActivityApi.Models.Category", "Category")
                        .WithMany("Activities")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FindActivityApi.Models.Comment", b =>
                {
                    b.HasOne("FindActivityApi.Models.Evnt", "Evnt")
                        .WithMany("Comments")
                        .HasForeignKey("EvntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindActivityApi.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evnt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindActivityApi.Models.Evnt", b =>
                {
                    b.HasOne("FindActivityApi.Models.Activity", "Activity")
                        .WithMany("Evnts")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindActivityApi.Models.User", "User")
                        .WithMany("Evnts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindActivityApi.Models.UserActivity", b =>
                {
                    b.HasOne("FindActivityApi.Models.Activity", "Activity")
                        .WithMany("UserActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FindActivityApi.Models.User", "User")
                        .WithMany("UserActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FindActivityApi.Models.Activity", b =>
                {
                    b.Navigation("Evnts");

                    b.Navigation("UserActivities");
                });

            modelBuilder.Entity("FindActivityApi.Models.Category", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("FindActivityApi.Models.Evnt", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("FindActivityApi.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Evnts");

                    b.Navigation("UserActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
