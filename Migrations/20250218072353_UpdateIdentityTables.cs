using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "802ac7ac-70a7-43f6-8f87-de9d19adca51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b69dbdeb-2dda-4438-b74f-2c88a4111d96");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3257bfdf-a85f-4614-9692-37fa88ba00d8", null, "Admin", "ADMIN" },
                    { "bbf2ace0-ac7e-4929-b93b-ce654b738fad", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3257bfdf-a85f-4614-9692-37fa88ba00d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbf2ace0-ac7e-4929-b93b-ce654b738fad");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "802ac7ac-70a7-43f6-8f87-de9d19adca51", null, "User", "USER" },
                    { "b69dbdeb-2dda-4438-b74f-2c88a4111d96", null, "Admin", "ADMIN" }
                });
        }
    }
}
