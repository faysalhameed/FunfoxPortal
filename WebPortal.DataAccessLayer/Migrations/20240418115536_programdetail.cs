using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.DataAccessLayer.Migrations
{
    public partial class programdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailId",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProgramDetails",
                newName: "ProgramId");

            migrationBuilder.RenameColumn(
                name: "ProgramDetailId",
                table: "Classes",
                newName: "ProgramDetailProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_ProgramDetailId",
                table: "Classes",
                newName: "IX_Classes_ProgramDetailProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailProgramId",
                table: "Classes",
                column: "ProgramDetailProgramId",
                principalTable: "ProgramDetails",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailProgramId",
                table: "Classes");

            migrationBuilder.RenameColumn(
                name: "ProgramId",
                table: "ProgramDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProgramDetailProgramId",
                table: "Classes",
                newName: "ProgramDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Classes_ProgramDetailProgramId",
                table: "Classes",
                newName: "IX_Classes_ProgramDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailId",
                table: "Classes",
                column: "ProgramDetailId",
                principalTable: "ProgramDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
