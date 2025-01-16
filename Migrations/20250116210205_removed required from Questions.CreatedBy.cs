using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class removedrequiredfromQuestionsCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07964eeb-887a-4ba1-ab25-5f904e72f98d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d4989c8-f004-4ea7-94a6-20273142d547");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "492fe823-f1e4-45dc-8da7-b9ad1427c9a5", null, "Admin", "ADMIN" },
                    { "d94458c4-48ae-49c2-9a82-7b139582a44b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "492fe823-f1e4-45dc-8da7-b9ad1427c9a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d94458c4-48ae-49c2-9a82-7b139582a44b");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Questions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "07964eeb-887a-4ba1-ab25-5f904e72f98d", null, "Admin", "ADMIN" },
                    { "2d4989c8-f004-4ea7-94a6-20273142d547", null, "User", "USER" }
                });
        }
    }
}
