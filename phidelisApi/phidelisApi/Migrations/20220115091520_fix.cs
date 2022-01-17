using Microsoft.EntityFrameworkCore.Migrations;

namespace phidelisApi.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdEnrol",
                table: "Enrols",
                newName: "IdEnrollment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdEnrollment",
                table: "Enrols",
                newName: "IdEnrol");
        }
    }
}
