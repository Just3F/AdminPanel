﻿// <auto-generated />
using AdminPanel.Const;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AdminPanel.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdminPanel.Models.tblUser", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<long>("UserVerificationId");

                    b.HasKey("PKID");

                    b.HasIndex("UserVerificationId");

                    b.ToTable("tblUser");
                });

            modelBuilder.Entity("AdminPanel.Models.tblUserVerification", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("EmailActivated");

                    b.Property<string>("EmailCode");

                    b.Property<bool>("PhoneActivated");

                    b.Property<string>("PhoneCode");

                    b.Property<DateTime?>("SentEmailCodeTime");

                    b.Property<DateTime?>("SentPhoneCodeTime");

                    b.HasKey("PKID");

                    b.ToTable("tblUserVerification");
                });

            modelBuilder.Entity("AdminPanel.Models.vlGeneralSettings", b =>
                {
                    b.Property<long>("PKID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsRequiredEmailVerification");

                    b.Property<bool>("IsRequiredPhoneVerification");

                    b.HasKey("PKID");

                    b.ToTable("vlGeneralSettings");
                });

            modelBuilder.Entity("AdminPanel.Models.tblUser", b =>
                {
                    b.HasOne("AdminPanel.Models.tblUserVerification", "UserVerification")
                        .WithMany()
                        .HasForeignKey("UserVerificationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
