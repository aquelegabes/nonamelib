using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoNameLib.Domain.Tests.Migrations
{
    /// <inheritdoc />
    public partial class TableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Domains",
                table: "Domains");

            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "TestDomain");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestDomain",
                table: "TestDomain",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TestDomain",
                table: "TestDomain");

            migrationBuilder.RenameTable(
                name: "TestDomain",
                newName: "Domains");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Domains",
                table: "Domains",
                column: "Id");
        }
    }
}
