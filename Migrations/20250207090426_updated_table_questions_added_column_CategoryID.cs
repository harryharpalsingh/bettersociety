using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class updated_table_questions_added_column_CategoryID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a232bff-16c4-4a6b-bc66-15f1c407060d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ad03a9d-7223-49bb-aa2b-22bf32879818");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "802ac7ac-70a7-43f6-8f87-de9d19adca51", null, "User", "USER" },
                    { "b69dbdeb-2dda-4438-b74f-2c88a4111d96", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "802ac7ac-70a7-43f6-8f87-de9d19adca51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b69dbdeb-2dda-4438-b74f-2c88a4111d96");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Questions");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a232bff-16c4-4a6b-bc66-15f1c407060d", null, "User", "USER" },
                    { "1ad03a9d-7223-49bb-aa2b-22bf32879818", null, "Admin", "ADMIN" }
                });
        }
    }
}
