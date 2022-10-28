using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class AuthDetailAuthTokenLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthDetailId",
                table: "AuthTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_AuthDetailId",
                table: "AuthTokens",
                column: "AuthDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_AuthDetails_AuthDetailId",
                table: "AuthTokens",
                column: "AuthDetailId",
                principalTable: "AuthDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_AuthDetails_AuthDetailId",
                table: "AuthTokens");

            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_AuthDetailId",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "AuthDetailId",
                table: "AuthTokens");
        }
    }
}
