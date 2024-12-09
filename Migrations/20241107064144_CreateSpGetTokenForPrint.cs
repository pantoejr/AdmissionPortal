#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpGetTokenForPrint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE PROC spGetTokenForPrint
                @TokenID INT
                AS
                BEGIN
                SELECT TOP(1) * FROM Tokens T WHERE T.Id = @TokenID;
                END"
            );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
