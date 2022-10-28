using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Modified_Account_Entity_Auth_Details_CID_CNAME : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_AuthDetails_AuthDetailId",
                table: "AuthTokens");

            migrationBuilder.RenameColumn(
                name: "AuthDetailId",
                table: "AuthTokens",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthTokens_AuthDetailId",
                table: "AuthTokens",
                newName: "IX_AuthTokens_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "AuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "AuthDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_AuthDetails_AccountId",
                table: "AuthTokens",
                column: "AccountId",
                principalTable: "AuthDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthTokens_AuthDetails_AccountId",
                table: "AuthTokens");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AuthTokens",
                newName: "AuthDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthTokens_AccountId",
                table: "AuthTokens",
                newName: "IX_AuthTokens_AuthDetailId");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "AuthDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CompanyId",
                table: "AuthDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthTokens_AuthDetails_AuthDetailId",
                table: "AuthTokens",
                column: "AuthDetailId",
                principalTable: "AuthDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
