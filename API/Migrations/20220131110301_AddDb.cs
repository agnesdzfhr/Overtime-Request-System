using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AddDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_department",
                columns: table => new
                {
                    DepartmentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtime_bonus",
                columns: table => new
                {
                    OvertimeBonusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionPct = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtime_bonus", x => x.OvertimeBonusID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtime_limit",
                columns: table => new
                {
                    OvertimeLimitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxOvertime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtime_limit", x => x.OvertimeLimitID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_employee",
                columns: table => new
                {
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<float>(type: "real", nullable: false),
                    WorkHourPerDay = table.Column<int>(type: "int", nullable: false),
                    WorkDayPerMonth = table.Column<int>(type: "int", nullable: false),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OvertimeLimitID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "tb_m_department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_employee_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_employee_tb_m_overtime_limit_OvertimeLimitID",
                        column: x => x.OvertimeLimitID,
                        principalTable: "tb_m_overtime_limit",
                        principalColumn: "OvertimeLimitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_overtime_request",
                columns: table => new
                {
                    OvertimeRequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_overtime_request", x => x.OvertimeRequestID);
                    table.ForeignKey(
                        name: "FK_tb_m_overtime_request_tb_m_employee_NIK",
                        column: x => x.NIK,
                        principalTable: "tb_m_employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_otp",
                columns: table => new
                {
                    OtpID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenOTP = table.Column<int>(type: "int", nullable: false),
                    ExpiredToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_otp", x => x.OtpID);
                    table.ForeignKey(
                        name: "FK_tb_m_otp_tb_m_account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "tb_m_account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_tr_account_role",
                columns: table => new
                {
                    AccountRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tr_account_role", x => x.AccountRoleID);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_role_tb_m_account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "tb_m_account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_tr_account_role_tb_m_role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "tb_m_role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_finance_validation",
                columns: table => new
                {
                    FinanceValidationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFee = table.Column<float>(type: "real", nullable: false),
                    OvertimeRequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_finance_validation", x => x.FinanceValidationID);
                    table.ForeignKey(
                        name: "FK_tb_m_finance_validation_tb_m_overtime_request_OvertimeRequestID",
                        column: x => x.OvertimeRequestID,
                        principalTable: "tb_m_overtime_request",
                        principalColumn: "OvertimeRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_manager_approval",
                columns: table => new
                {
                    ManagerApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    OvertimeRequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_manager_approval", x => x.ManagerApprovalID);
                    table.ForeignKey(
                        name: "FK_tb_m_manager_approval_tb_m_overtime_request_OvertimeRequestID",
                        column: x => x.OvertimeRequestID,
                        principalTable: "tb_m_overtime_request",
                        principalColumn: "OvertimeRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_NIK",
                table: "tb_m_account",
                column: "NIK",
                unique: true,
                filter: "[NIK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_DepartmentID",
                table: "tb_m_employee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_ManagerID",
                table: "tb_m_employee",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_employee_OvertimeLimitID",
                table: "tb_m_employee",
                column: "OvertimeLimitID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_finance_validation_OvertimeRequestID",
                table: "tb_m_finance_validation",
                column: "OvertimeRequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_manager_approval_OvertimeRequestID",
                table: "tb_m_manager_approval",
                column: "OvertimeRequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_otp_AccountID",
                table: "tb_m_otp",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_overtime_request_NIK",
                table: "tb_m_overtime_request",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_role_AccountID",
                table: "tb_tr_account_role",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_tb_tr_account_role_RoleID",
                table: "tb_tr_account_role",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_finance_validation");

            migrationBuilder.DropTable(
                name: "tb_m_manager_approval");

            migrationBuilder.DropTable(
                name: "tb_m_otp");

            migrationBuilder.DropTable(
                name: "tb_m_overtime_bonus");

            migrationBuilder.DropTable(
                name: "tb_tr_account_role");

            migrationBuilder.DropTable(
                name: "tb_m_overtime_request");

            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_employee");

            migrationBuilder.DropTable(
                name: "tb_m_department");

            migrationBuilder.DropTable(
                name: "tb_m_overtime_limit");
        }
    }
}
