using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Added_Table_Auth_Detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AuthTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuthDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LastSynced = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateUTC = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthTokens_CompanyId",
                table: "AuthTokens",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_AuthDetails_CompanyId",
                table: "AuthTokens",
                column: "CompanyId",
                principalTable: "AuthDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_AuthDetails_CompanyId",
                table: "AuthTokens");

            migrationBuilder.DropTable(
                name: "AuthDetails");

            migrationBuilder.DropIndex(
                name: "IX_AuthTokens_CompanyId",
                table: "AuthTokens");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AuthTokens");
        }
    }
}
