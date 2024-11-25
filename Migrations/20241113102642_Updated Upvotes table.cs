using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bettersociety.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUpvotestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteType",
                table: "Votes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteType",
                table: "Votes");
        }
    }
}
