#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTokenFieldAddStudentID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Applicants",
                newName: "StudentID");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Applicants");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Applicants",
                newName: "StudentId");
        }
    }
}
