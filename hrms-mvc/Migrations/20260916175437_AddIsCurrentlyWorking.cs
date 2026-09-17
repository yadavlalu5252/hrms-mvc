using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrms_mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddIsCurrentlyWorking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentlyWorking",
                table: "Experiences",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentlyWorking",
                table: "Experiences");
        }
    }
}
