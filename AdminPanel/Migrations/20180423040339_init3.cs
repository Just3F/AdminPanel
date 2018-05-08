using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdminPanel.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUserVerification_tblUser_UserId",
                table: "tblUserVerification");

            migrationBuilder.DropIndex(
                name: "IX_tblUserVerification_UserId",
                table: "tblUserVerification");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblUserVerification");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "tblUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UserVerificationId",
                table: "tblUser",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_UserVerificationId",
                table: "tblUser",
                column: "UserVerificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_tblUserVerification_UserVerificationId",
                table: "tblUser",
                column: "UserVerificationId",
                principalTable: "tblUserVerification",
                principalColumn: "PKID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_tblUserVerification_UserVerificationId",
                table: "tblUser");

            migrationBuilder.DropIndex(
                name: "IX_tblUser_UserVerificationId",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "UserVerificationId",
                table: "tblUser");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "tblUserVerification",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_tblUserVerification_UserId",
                table: "tblUserVerification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUserVerification_tblUser_UserId",
                table: "tblUserVerification",
                column: "UserId",
                principalTable: "tblUser",
                principalColumn: "PKID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
