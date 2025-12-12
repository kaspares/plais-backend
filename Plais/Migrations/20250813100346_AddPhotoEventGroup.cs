using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plais.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoEventGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "EventGroups",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "EventGroups");
        }
    }
}
