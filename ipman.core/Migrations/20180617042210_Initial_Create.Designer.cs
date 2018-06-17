﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ipman.core.Utilities;

namespace ipman.core.Migrations
{
    [DbContext(typeof(IPManDataContext))]
    [Migration("20180617042210_Initial_Create")]
    partial class Initial_Create
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ipman.shared.Entity.Department", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedUTC");

                    b.Property<string>("DepartmentName");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastUpdatedUTC");

                    b.Property<Guid>("SiteAccountID");

                    b.HasKey("ID");

                    b.HasIndex("SiteAccountID");

                    b.ToTable("Departments");

                    b.HasData(
                        new { ID = new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), DepartmentName = "I AM THE WALRUS", IsActive = true, LastUpdatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), SiteAccountID = new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8") }
                    );
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.PostTag", b =>
                {
                    b.Property<Guid>("PostID");

                    b.Property<Guid>("TagID");

                    b.Property<Guid>("ID");

                    b.HasKey("PostID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("PostTag");
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.SiteAccountUserAccount", b =>
                {
                    b.Property<Guid>("SiteAccountID");

                    b.Property<Guid>("UserAccountID");

                    b.Property<DateTime>("CreatedUTC");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsMemberOfAllDepartments");

                    b.Property<DateTime>("LastLoginUTC");

                    b.Property<Guid>("RoleID");

                    b.HasKey("SiteAccountID", "UserAccountID");

                    b.HasIndex("UserAccountID");

                    b.ToTable("SiteAccountUserAccount");

                    b.HasData(
                        new { SiteAccountID = new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), UserAccountID = new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = true, IsMemberOfAllDepartments = true, LastLoginUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), RoleID = new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") },
                        new { SiteAccountID = new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), UserAccountID = new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = true, IsMemberOfAllDepartments = true, LastLoginUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), RoleID = new Guid("2fa6b838-8b5b-4bb0-b888-ffce4fb7b7b7") }
                    );
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.SiteAccountUserAccountDepartment", b =>
                {
                    b.Property<Guid>("SiteAccountUserAccountID");

                    b.Property<Guid>("DepartmentID");

                    b.Property<DateTime>("CreatedUTC");

                    b.Property<bool>("IsActive");

                    b.Property<Guid?>("SiteAccountUserSiteAccountID");

                    b.Property<Guid?>("SiteAccountUserUserAccountID");

                    b.HasKey("SiteAccountUserAccountID", "DepartmentID");

                    b.HasIndex("DepartmentID");

                    b.HasIndex("SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID");

                    b.ToTable("SiteAccountUserAccountDepartment");

                    b.HasData(
                        new { SiteAccountUserAccountID = new Guid("143bf075-478f-4e1a-b8d8-889be2af42a4"), DepartmentID = new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = true },
                        new { SiteAccountUserAccountID = new Guid("f9c6aae5-fe6c-43bf-92b4-bf79ba92d2fe"), DepartmentID = new Guid("e29241de-853c-4a05-9928-1c6ac7f73b25"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = true }
                    );
                });

            modelBuilder.Entity("ipman.shared.Entity.Post", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostDescription");

                    b.Property<string>("PostTitle");

                    b.HasKey("ID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ipman.shared.Entity.SiteAccount", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedUTC");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastUpdatedUTC");

                    b.Property<string>("SiteAccountName");

                    b.HasKey("ID");

                    b.ToTable("SiteAccounts");

                    b.HasData(
                        new { ID = new Guid("4a93afc2-8ef0-4d91-9374-67e60fc336a8"), CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), IsActive = true, LastUpdatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), SiteAccountName = "Awesome Possum Admins" }
                    );
                });

            modelBuilder.Entity("ipman.shared.Entity.Tag", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TagName");

                    b.HasKey("ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ipman.shared.Entity.UserAccount", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarLink");

                    b.Property<DateTime>("CreatedUTC");

                    b.Property<string>("EmailAddress");

                    b.Property<string>("FirstName");

                    b.Property<string>("GitHubID");

                    b.Property<string>("GoogleID");

                    b.Property<int>("LastLoginProvider");

                    b.Property<DateTime>("LastLoginUTC");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastUpdatedUTC");

                    b.HasKey("ID");

                    b.ToTable("UserAccounts");

                    b.HasData(
                        new { ID = new Guid("4d8881bd-db0a-4725-9cf0-2c4390013a30"), AvatarLink = "https://lh4.googleusercontent.com/-gPvw9sU8Mpc/AAAAAAAAAAI/AAAAAAAAAOY/HQ2yjXFKeEk/photo.jpg?sz=50", CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), EmailAddress = "budnjoe@gmail.com", FirstName = "rob", LastLoginProvider = 0, LastLoginUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Hardesty", LastUpdatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                        new { ID = new Guid("f84d5d8f-131e-4554-a45d-e1d03c02bc43"), AvatarLink = "https://lh3.googleusercontent.com/-pu8oCttY3pE/AAAAAAAAAAI/AAAAAAAAAAA/h5YVW6XWCK4/photo.jpg?sz=50", CreatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), EmailAddress = "robert.hardesty.mail@gmail.com", FirstName = "Robert", LastLoginProvider = 0, LastLoginUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), LastName = "Hardesty", LastUpdatedUTC = new DateTime(2018, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                    );
                });

            modelBuilder.Entity("ipman.shared.Entity.Department", b =>
                {
                    b.HasOne("ipman.shared.Entity.SiteAccount", "SiteAccount")
                        .WithMany()
                        .HasForeignKey("SiteAccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.PostTag", b =>
                {
                    b.HasOne("ipman.shared.Entity.Post", "Post")
                        .WithMany("PostTag")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ipman.shared.Entity.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.SiteAccountUserAccount", b =>
                {
                    b.HasOne("ipman.shared.Entity.SiteAccount", "SiteAccount")
                        .WithMany("SiteAccountUserAccounts")
                        .HasForeignKey("SiteAccountID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ipman.shared.Entity.UserAccount", "UserAccount")
                        .WithMany("SiteAccountUserAccounts")
                        .HasForeignKey("UserAccountID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ipman.shared.Entity.Join.SiteAccountUserAccountDepartment", b =>
                {
                    b.HasOne("ipman.shared.Entity.Department", "Department")
                        .WithMany("SiteAccountUserAccountDepartments")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ipman.shared.Entity.Join.SiteAccountUserAccount", "SiteAccountUser")
                        .WithMany("SiteAccountUserAccountDepartments")
                        .HasForeignKey("SiteAccountUserSiteAccountID", "SiteAccountUserUserAccountID");
                });
#pragma warning restore 612, 618
        }
    }
}