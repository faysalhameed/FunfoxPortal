using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.DataAccessLayer.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Classes",
                newName: "ClassId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassId",
                table: "Classes",
                newName: "Id");
        }
    }
}
