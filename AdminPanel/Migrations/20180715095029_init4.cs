using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdminPanel.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MainCategoryId",
                table: "tblCategory",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblMainCategory",
                columns: table => new
                {
                    PKID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMainCategory", x => x.PKID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCategory_MainCategoryId",
                table: "tblCategory",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCategory_tblMainCategory_MainCategoryId",
                table: "tblCategory",
                column: "MainCategoryId",
                principalTable: "tblMainCategory",
                principalColumn: "PKID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCategory_tblMainCategory_MainCategoryId",
                table: "tblCategory");

            migrationBuilder.DropTable(
                name: "tblMainCategory");

            migrationBuilder.DropIndex(
                name: "IX_tblCategory_MainCategoryId",
                table: "tblCategory");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "tblCategory");
        }
    }
}
