using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class changeAtribut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalHour",
                table: "tb_m_overtime_request",
                newName: "TotalTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalTime",
                table: "tb_m_overtime_request",
                newName: "TotalHour");
        }
    }
}
