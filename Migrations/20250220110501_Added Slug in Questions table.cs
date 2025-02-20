using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class AddedSluginQuestionstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3257bfdf-a85f-4614-9692-37fa88ba00d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbf2ace0-ac7e-4929-b93b-ce654b738fad");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "325adc7c-a60f-471c-9a04-6c097dc7fced", null, "Admin", "ADMIN" },
                    { "b17ac0e4-41fd-4cd7-b0ac-e9a52258a47e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "325adc7c-a60f-471c-9a04-6c097dc7fced");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b17ac0e4-41fd-4cd7-b0ac-e9a52258a47e");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Questions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3257bfdf-a85f-4614-9692-37fa88ba00d8", null, "Admin", "ADMIN" },
                    { "bbf2ace0-ac7e-4929-b93b-ce654b738fad", null, "User", "USER" }
                });
        }
    }
}
