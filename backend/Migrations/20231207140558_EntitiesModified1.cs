using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace housingCooperative.Migrations
{
    public partial class EntitiesModified1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "tblCustomerEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblCustomerEntity");

            migrationBuilder.RenameColumn(
                name: "EstimatedStartTime",
                table: "tblPhaseEntity",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "EstimatedEndTime",
                table: "tblPhaseEntity",
                newName: "EstimatedStartDate");

            migrationBuilder.AddColumn<long>(
                name: "InstalmentAmount",
                table: "tblPlotEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PrePaymentAmount",
                table: "tblPlotEntity",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblPhaseEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "tblPhaseEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedEndDate",
                table: "tblPhaseEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "tblLandProjectEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedEndDate",
                table: "tblLandProjectEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedStartDate",
                table: "tblLandProjectEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "tblLandProjectEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "tblLandProjectEntity",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "tblCustomerEntity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstalmentAmount",
                table: "tblPlotEntity");

            migrationBuilder.DropColumn(
                name: "PrePaymentAmount",
                table: "tblPlotEntity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tblPhaseEntity");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "tblPhaseEntity");

            migrationBuilder.DropColumn(
                name: "EstimatedEndDate",
                table: "tblPhaseEntity");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "tblLandProjectEntity");

            migrationBuilder.DropColumn(
                name: "EstimatedEndDate",
                table: "tblLandProjectEntity");

            migrationBuilder.DropColumn(
                name: "EstimatedStartDate",
                table: "tblLandProjectEntity");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "tblLandProjectEntity");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "tblLandProjectEntity");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "tblPhaseEntity",
                newName: "EstimatedStartTime");

            migrationBuilder.RenameColumn(
                name: "EstimatedStartDate",
                table: "tblPhaseEntity",
                newName: "EstimatedEndTime");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "tblCustomerEntity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tblCustomerEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tblCustomerEntity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
