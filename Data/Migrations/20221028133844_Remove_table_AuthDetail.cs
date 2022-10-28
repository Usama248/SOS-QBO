using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Remove_table_AuthDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_AuthDetails_AccountId",
                table: "AuthTokens");

            migrationBuilder.DropTable(
                name: "AuthDetails");

            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_AccountId",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "AuthTokens");

            migrationBuilder.AlterColumn<long>(
                name: "ExpiresIn",
                table: "AuthTokens",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CompanyTypeEnum",
                table: "AuthTokens",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyTypeEnum",
                table: "AuthTokens");

            migrationBuilder.AlterColumn<string>(
                name: "ExpiresIn",
                table: "AuthTokens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "AuthTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AuthDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    LastSynced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_AccountId",
                table: "AuthTokens",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_AuthDetails_AccountId",
                table: "AuthTokens",
                column: "AccountId",
                principalTable: "AuthDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
