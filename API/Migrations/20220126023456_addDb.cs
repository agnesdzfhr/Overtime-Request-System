using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Department",
                columns: table => new
                {
                    Department_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Department", x => x.Department_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Overtime",
                columns: table => new
                {
                    Overtime_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionPct = table.Column<float>(type: "real", nullable: false),
                    MaxOvertime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Overtime", x => x.Overtime_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OvertimeBonus",
                columns: table => new
                {
                    OvertimeBonus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalBonus = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_OvertimeBonus", x => x.OvertimeBonus_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    Role_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.Role_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<float>(type: "real", nullable: false),
                    Department_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Manager_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Department_Department_ID",
                        column: x => x.Department_ID,
                        principalTable: "TB_M_Department",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Employee_Manager_ID",
                        column: x => x.Manager_ID,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OvertimeSchedule",
                columns: table => new
                {
                    OvertimeSchedule_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsApprove = table.Column<bool>(type: "bit", nullable: false),
                    Overtime_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OvertimeBonus_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_OvertimeSchedule", x => x.OvertimeSchedule_ID);
                    table.ForeignKey(
                        name: "FK_TB_M_OvertimeSchedule_TB_M_Overtime_Overtime_ID",
                        column: x => x.Overtime_ID,
                        principalTable: "TB_M_Overtime",
                        principalColumn: "Overtime_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_OvertimeSchedule_TB_M_OvertimeBonus_OvertimeBonus_ID",
                        column: x => x.OvertimeBonus_ID,
                        principalTable: "TB_M_OvertimeBonus",
                        principalColumn: "OvertimeBonus_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    Account_ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<int>(type: "int", nullable: true),
                    ExpiredToken = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.Account_ID);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_EmployeeOvertimeSchedule",
                columns: table => new
                {
                    EmployeeOvertimeSchedule_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OvertimeSchedule_ID = table.Column<int>(type: "int", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_EmployeeOvertimeSchedule", x => x.EmployeeOvertimeSchedule_ID);
                    table.ForeignKey(
                        name: "FK_TB_TR_EmployeeOvertimeSchedule_TB_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TR_EmployeeOvertimeSchedule_TB_M_OvertimeSchedule_OvertimeSchedule_ID",
                        column: x => x.OvertimeSchedule_ID,
                        principalTable: "TB_M_OvertimeSchedule",
                        principalColumn: "OvertimeSchedule_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_AccountRole",
                columns: table => new
                {
                    AccountRole_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Account_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_AccountRole", x => x.AccountRole_ID);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Account_Account_ID",
                        column: x => x.Account_ID,
                        principalTable: "TB_M_Account",
                        principalColumn: "Account_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Role_Role_ID",
                        column: x => x.Role_ID,
                        principalTable: "TB_M_Role",
                        principalColumn: "Role_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_NIK",
                table: "TB_M_Account",
                column: "NIK",
                unique: true,
                filter: "[NIK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_Department_ID",
                table: "TB_M_Employee",
                column: "Department_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_Manager_ID",
                table: "TB_M_Employee",
                column: "Manager_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_OvertimeSchedule_Overtime_ID",
                table: "TB_M_OvertimeSchedule",
                column: "Overtime_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_OvertimeSchedule_OvertimeBonus_ID",
                table: "TB_M_OvertimeSchedule",
                column: "OvertimeBonus_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_Account_ID",
                table: "TB_TR_AccountRole",
                column: "Account_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_Role_ID",
                table: "TB_TR_AccountRole",
                column: "Role_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_EmployeeOvertimeSchedule_NIK",
                table: "TB_TR_EmployeeOvertimeSchedule",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_EmployeeOvertimeSchedule_OvertimeSchedule_ID",
                table: "TB_TR_EmployeeOvertimeSchedule",
                column: "OvertimeSchedule_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TR_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_TR_EmployeeOvertimeSchedule");

            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_OvertimeSchedule");

            migrationBuilder.DropTable(
                name: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Overtime");

            migrationBuilder.DropTable(
                name: "TB_M_OvertimeBonus");

            migrationBuilder.DropTable(
                name: "TB_M_Department");
        }
    }
}
