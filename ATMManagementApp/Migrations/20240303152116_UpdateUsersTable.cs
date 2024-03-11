using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATMManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BranchNumber",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FINumber",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "parish",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BranchNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FINumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "parish",
                table: "AspNetUsers");

            

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ac00f50-a4c0-4965-aefb-8bb2a60de030", null, "auditor", "auditor" },
                    { "78211cc4-b15a-4e35-b7ee-ba39c65d84e3", null, "admin", "admin" },
                    { "c767f940-5608-4749-a98c-2a145d908c8c", null, "fi", "fi" }
                });
        }
    }
}
