#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace AdmissionPortal.Migrations
{
    /// <inheritdoc />
    public partial class CreateSpUpdateUserRolesProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE sp_UpdateUserRoles
                @GroupID INT,
                @UserID Nvarchar(255),
                @ResultMessage INT OUTPUT
                    AS
                    BEGIN
                SET NOCOUNT ON;

                IF NOT EXISTS (SELECT 1 FROM AspNetUsers WHERE Id = @UserID)
                BEGIN
                    SET @ResultMessage = 500;
                    RETURN;
                END

                DELETE FROM AspNetUserRoles WHERE UserId = @UserID;
	            DELETE FROM GroupUsers WHERE AppUserID = @UserID;

	            INSERT INTO GroupUsers (GroupID,AppUserID,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,IsActive) 
	            VALUES (@GroupID,@UserID,'storedprocedure',GETDATE(),'storedprocedure',GETDATE(),1);

                DECLARE @RoleId NVARCHAR(255);

                DECLARE cur CURSOR FOR 
                    SELECT r.Id
                    FROM GroupRoles gr
                    INNER JOIN AspNetRoles r ON gr.RoleID = r.Id
                    WHERE gr.GroupID = @GroupID;
                OPEN cur;
                FETCH NEXT FROM cur INTO @RoleId

                WHILE @@FETCH_STATUS = 0
                BEGIN
                    INSERT INTO AspNetUserRoles (UserId, RoleId)
                    VALUES (@UserID, @RoleId)
                    FETCH NEXT FROM cur INTO @RoleId;
                END
                CLOSE cur;
                DEALLOCATE cur;
                SET @ResultMessage = 100;
            END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_UpdateUserRoles");
        }
    }
}
