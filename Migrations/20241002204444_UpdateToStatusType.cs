#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class UpdateToStatusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Applicants");

            migrationBuilder.AddColumn<int>(
                name: "StatusTypeID",
                table: "Applicants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_StatusTypeID",
                table: "Applicants",
                column: "StatusTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_StatusTypes_StatusTypeID",
                table: "Applicants",
                column: "StatusTypeID",
                principalTable: "StatusTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_StatusTypes_StatusTypeID",
                table: "Applicants");

            migrationBuilder.DropTable(
                name: "StatusTypes");

            migrationBuilder.DropIndex(
                name: "IX_Applicants_StatusTypeID",
                table: "Applicants");

            migrationBuilder.DropColumn(
                name: "StatusTypeID",
                table: "Applicants");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Applicants",
                type: "bit",
                nullable: true);
        }
    }
}
