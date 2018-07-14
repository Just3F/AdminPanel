using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdminPanel.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblUserVerification",
                columns: table => new
                {
                    PKID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmailActivated = table.Column<bool>(nullable: false),
                    EmailCode = table.Column<string>(nullable: true),
                    PhoneActivated = table.Column<bool>(nullable: false),
                    PhoneCode = table.Column<string>(nullable: true),
                    SentEmailCodeTime = table.Column<DateTime>(nullable: true),
                    SentPhoneCodeTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserVerification", x => x.PKID);
                });

            migrationBuilder.CreateTable(
                name: "vlGeneralSettings",
                columns: table => new
                {
                    PKID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsRequiredEmailVerification = table.Column<bool>(nullable: false),
                    IsRequiredPhoneVerification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vlGeneralSettings", x => x.PKID);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    PKID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false),
                    UserVerificationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.PKID);
                    table.ForeignKey(
                        name: "FK_tblUser_tblUserVerification_UserVerificationId",
                        column: x => x.UserVerificationId,
                        principalTable: "tblUserVerification",
                        principalColumn: "PKID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_UserVerificationId",
                table: "tblUser",
                column: "UserVerificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "vlGeneralSettings");

            migrationBuilder.DropTable(
                name: "tblUserVerification");
        }
    }
}
