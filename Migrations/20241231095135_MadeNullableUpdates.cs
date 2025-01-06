using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class MadeNullableUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_MaritalStatuses_MaritalStatusID",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_RelationshipTypes_RelationshipTypeID",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_ReligionTypes_ReligionID",
                table: "Applicants");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionID",
                table: "Applicants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RelationshipTypeID",
                table: "Applicants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusID",
                table: "Applicants",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentAddress",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_MaritalStatuses_MaritalStatusID",
                table: "Applicants",
                column: "MaritalStatusID",
                principalTable: "MaritalStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_RelationshipTypes_RelationshipTypeID",
                table: "Applicants",
                column: "RelationshipTypeID",
                principalTable: "RelationshipTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_ReligionTypes_ReligionID",
                table: "Applicants",
                column: "ReligionID",
                principalTable: "ReligionTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_MaritalStatuses_MaritalStatusID",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_RelationshipTypes_RelationshipTypeID",
                table: "Applicants");

            migrationBuilder.DropForeignKey(
                name: "FK_Applicants_ReligionTypes_ReligionID",
                table: "Applicants");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionID",
                table: "Applicants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RelationshipTypeID",
                table: "Applicants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusID",
                table: "Applicants",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentAddress",
                table: "Applicants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_MaritalStatuses_MaritalStatusID",
                table: "Applicants",
                column: "MaritalStatusID",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_RelationshipTypes_RelationshipTypeID",
                table: "Applicants",
                column: "RelationshipTypeID",
                principalTable: "RelationshipTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Applicants_ReligionTypes_ReligionID",
                table: "Applicants",
                column: "ReligionID",
                principalTable: "ReligionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
