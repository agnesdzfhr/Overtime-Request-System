using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class fixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalHour",
                table: "tb_m_overtime_request",
                newName: "TotalHour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalHour",
                table: "tb_m_overtime_request",
                newName: "totalHour");
        }
    }
}
