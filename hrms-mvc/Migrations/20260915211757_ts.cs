using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrms_mvc.Migrations
{
    /// <inheritdoc />
    public partial class ts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheet_Users_UserId",
                table: "Timesheet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timesheet",
                table: "Timesheet");

            migrationBuilder.RenameTable(
                name: "Timesheet",
                newName: "Timesheets");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheet_UserId",
                table: "Timesheets",
                newName: "IX_Timesheets_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timesheets",
                table: "Timesheets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Timesheets_Users_UserId",
                table: "Timesheets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Timesheets",
                table: "Timesheets");

            migrationBuilder.RenameTable(
                name: "Timesheets",
                newName: "Timesheet");

            migrationBuilder.RenameIndex(
                name: "IX_Timesheets_UserId",
                table: "Timesheet",
                newName: "IX_Timesheet_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Timesheet",
                table: "Timesheet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Timesheet_Users_UserId",
                table: "Timesheet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
