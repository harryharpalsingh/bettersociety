using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class AddedModelAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "325adc7c-a60f-471c-9a04-6c097dc7fced");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b17ac0e4-41fd-4cd7-b0ac-e9a52258a47e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "916a1c1f-7e12-426d-8a47-bbb8fed209ac", null, "User", "USER" },
                    { "ee459511-5929-4c39-9a43-bf2db73a45ad", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsXrefTags_QuestionId",
                table: "QuestionsXrefTags",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsXrefTags_TagId",
                table: "QuestionsXrefTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CategoryID",
                table: "Questions",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionCategories_CategoryID",
                table: "Questions",
                column: "CategoryID",
                principalTable: "QuestionCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsXrefTags_Questions_QuestionId",
                table: "QuestionsXrefTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsXrefTags_Tags_TagId",
                table: "QuestionsXrefTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionCategories_CategoryID",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsXrefTags_Questions_QuestionId",
                table: "QuestionsXrefTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsXrefTags_Tags_TagId",
                table: "QuestionsXrefTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsXrefTags_QuestionId",
                table: "QuestionsXrefTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionsXrefTags_TagId",
                table: "QuestionsXrefTags");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CategoryID",
                table: "Questions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "916a1c1f-7e12-426d-8a47-bbb8fed209ac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee459511-5929-4c39-9a43-bf2db73a45ad");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "325adc7c-a60f-471c-9a04-6c097dc7fced", null, "Admin", "ADMIN" },
                    { "b17ac0e4-41fd-4cd7-b0ac-e9a52258a47e", null, "User", "USER" }
                });
        }
    }
}
