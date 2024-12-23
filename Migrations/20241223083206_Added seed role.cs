using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class Addedseedrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07964eeb-887a-4ba1-ab25-5f904e72f98d", null, "Admin", "ADMIN" },
                    { "2d4989c8-f004-4ea7-94a6-20273142d547", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07964eeb-887a-4ba1-ab25-5f904e72f98d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d4989c8-f004-4ea7-94a6-20273142d547");
        }
    }
}
