using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.DataAccessLayer.Migrations
{
    public partial class Updt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailProgramId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ProgramDetailProgramId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ProgramDetailProgramId",
                table: "Classes");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProgramId",
                table: "Classes",
                column: "ProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramId",
                table: "Classes",
                column: "ProgramId",
                principalTable: "ProgramDetails",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_ProgramId",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "ProgramDetailProgramId",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ProgramDetailProgramId",
                table: "Classes",
                column: "ProgramDetailProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_ProgramDetails_ProgramDetailProgramId",
                table: "Classes",
                column: "ProgramDetailProgramId",
                principalTable: "ProgramDetails",
                principalColumn: "ProgramId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
