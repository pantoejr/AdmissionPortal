using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class MadeNua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TitleTypes_TitleID",
                table: "Applicants");

            migrationBuilder.AlterColumn<int>(
                name: "TitleID",
                table: "Applicants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TitleTypes_TitleID",
                table: "Applicants",
                column: "TitleID",
                principalTable: "TitleTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_TitleTypes_TitleID",
                table: "Applicants");

            migrationBuilder.AlterColumn<int>(
                name: "TitleID",
                table: "Applicants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_TitleTypes_TitleID",
                table: "Applicants",
                column: "TitleID",
                principalTable: "TitleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
