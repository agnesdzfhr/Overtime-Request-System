using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addAtributTotalHour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "totalHour",
                table: "tb_m_overtime_request",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "totalHour",
                table: "tb_m_overtime_request");
        }
    }
}
