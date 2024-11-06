﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudHRMS.Migrations
{
    /// <inheritdoc />
    public partial class increasetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftAssign",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 15, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", maxLength: 15, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShiftAssign_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftAssign_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssign_EmployeeId",
                table: "ShiftAssign",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftAssign_ShiftId",
                table: "ShiftAssign",
                column: "ShiftId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftAssign");
        }
    }
}
