using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations;

/// <inheritdoc />
public partial class UpdatedAppUserIdColumn : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey("PK_AppUsers", "AppUsers");

        migrationBuilder.AlterColumn<Guid>(
            name: "Id",
            table: "AppUsers",
            type: "uniqueidentifier",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        // Add the new primary key
        migrationBuilder.AddPrimaryKey(
            name: "PK_AppUsers",
            table: "AppUsers",
            columns: ["Id"]
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Id",
            table: "AppUsers",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(Guid),
            oldType: "uniqueidentifier");
    }
}
