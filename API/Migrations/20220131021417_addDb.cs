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
                    DepartmentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OvertimeBonus",
                columns: table => new
                {
                    OvertimeBonusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Hour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommisionPct = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_OvertimeBonus", x => x.OvertimeBonusID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OvertimeLimit",
                columns: table => new
                {
                    OvertimeLimitID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxOvertime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_OvertimeLimit", x => x.OvertimeLimitID);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Role", x => x.RoleID);
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
                    WorkHourPerDay = table.Column<int>(type: "int", nullable: false),
                    WorkDayPerMonth = table.Column<int>(type: "int", nullable: false),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DepartmentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OvertimeLimitID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Employee", x => x.NIK);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "TB_M_Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_Employee_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Employee_TB_M_OvertimeLimit_OvertimeLimitID",
                        column: x => x.OvertimeLimitID,
                        principalTable: "TB_M_OvertimeLimit",
                        principalColumn: "OvertimeLimitID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Account",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIK = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Account", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_TB_M_Account_TB_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OvertimeRequest",
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
                    table.PrimaryKey("PK_TB_M_OvertimeRequest", x => x.OvertimeRequestID);
                    table.ForeignKey(
                        name: "FK_TB_M_OvertimeRequest_TB_M_Employee_NIK",
                        column: x => x.NIK,
                        principalTable: "TB_M_Employee",
                        principalColumn: "NIK",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_OTP",
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
                    table.PrimaryKey("PK_TB_M_OTP", x => x.OtpID);
                    table.ForeignKey(
                        name: "FK_TB_M_OTP_TB_M_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "TB_M_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_TR_AccountRole",
                columns: table => new
                {
                    AccountRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TR_AccountRole", x => x.AccountRoleID);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "TB_M_Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_TR_AccountRole_TB_M_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "TB_M_Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_FinanceValidation",
                columns: table => new
                {
                    FinanceValidationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OvertimeRequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_FinanceValidation", x => x.FinanceValidationID);
                    table.ForeignKey(
                        name: "FK_TB_M_FinanceValidation_TB_M_OvertimeRequest_OvertimeRequestID",
                        column: x => x.OvertimeRequestID,
                        principalTable: "TB_M_OvertimeRequest",
                        principalColumn: "OvertimeRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_ManagerApproval",
                columns: table => new
                {
                    ManagerApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManagerApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    OvertimeRequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_ManagerApproval", x => x.ManagerApprovalID);
                    table.ForeignKey(
                        name: "FK_TB_M_ManagerApproval_TB_M_OvertimeRequest_OvertimeRequestID",
                        column: x => x.OvertimeRequestID,
                        principalTable: "TB_M_OvertimeRequest",
                        principalColumn: "OvertimeRequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Account_NIK",
                table: "TB_M_Account",
                column: "NIK",
                unique: true,
                filter: "[NIK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_DepartmentID",
                table: "TB_M_Employee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_ManagerID",
                table: "TB_M_Employee",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Employee_OvertimeLimitID",
                table: "TB_M_Employee",
                column: "OvertimeLimitID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_FinanceValidation_OvertimeRequestID",
                table: "TB_M_FinanceValidation",
                column: "OvertimeRequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_ManagerApproval_OvertimeRequestID",
                table: "TB_M_ManagerApproval",
                column: "OvertimeRequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_OTP_AccountID",
                table: "TB_M_OTP",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_OvertimeRequest_NIK",
                table: "TB_M_OvertimeRequest",
                column: "NIK");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_AccountID",
                table: "TB_TR_AccountRole",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TR_AccountRole_RoleID",
                table: "TB_TR_AccountRole",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_FinanceValidation");

            migrationBuilder.DropTable(
                name: "TB_M_ManagerApproval");

            migrationBuilder.DropTable(
                name: "TB_M_OTP");

            migrationBuilder.DropTable(
                name: "TB_M_OvertimeBonus");

            migrationBuilder.DropTable(
                name: "TB_TR_AccountRole");

            migrationBuilder.DropTable(
                name: "TB_M_OvertimeRequest");

            migrationBuilder.DropTable(
                name: "TB_M_Account");

            migrationBuilder.DropTable(
                name: "TB_M_Role");

            migrationBuilder.DropTable(
                name: "TB_M_Employee");

            migrationBuilder.DropTable(
                name: "TB_M_Department");

            migrationBuilder.DropTable(
                name: "TB_M_OvertimeLimit");
        }
    }
}
