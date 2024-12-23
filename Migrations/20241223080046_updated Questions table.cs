using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class updatedQuestionstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QuestionDetail",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionDetail",
                table: "Questions");
        }
    }
}
