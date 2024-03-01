using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ATMManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ac00f50-a4c0-4965-aefb-8bb2a60de030");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78211cc4-b15a-4e35-b7ee-ba39c65d84e3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c767f940-5608-4749-a98c-2a145d908c8c");
        }
    }
}
