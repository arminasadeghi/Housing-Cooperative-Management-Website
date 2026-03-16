using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace housingCooperative.Migrations
{
    public partial class Init_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCustomerEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCustomerEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLandProjectEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    ImageIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLandProjectEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPhaseEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Progress = table.Column<float>(type: "real", nullable: true),
                    EstimatedStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPhaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPhaseEntity_tblLandProjectEntity_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblLandProjectEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblPlotEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Meterage = table.Column<long>(type: "bigint", nullable: true),
                    Value = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPlotEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPlotEntity_tblLandProjectEntity_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblLandProjectEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblContractEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalCommitedAmount = table.Column<long>(type: "bigint", nullable: true),
                    TotalPaidAmount = table.Column<long>(type: "bigint", nullable: true),
                    PrePaymentAmount = table.Column<long>(type: "bigint", nullable: true),
                    InstalmentAmount = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContractEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContractEntity_tblCustomerEntity_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tblCustomerEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblContractEntity_tblLandProjectEntity_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "tblLandProjectEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblContractEntity_tblPlotEntity_PlotId",
                        column: x => x.PlotId,
                        principalTable: "tblPlotEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblContractItemEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstalmentAmount = table.Column<long>(type: "bigint", nullable: true),
                    PaidAmount = table.Column<long>(type: "bigint", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContractItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContractItemEntity_tblContractEntity_ContractId",
                        column: x => x.ContractId,
                        principalTable: "tblContractEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tblContractPaidItemEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsVisibled = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblContractPaidItemEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblContractPaidItemEntity_tblContractItemEntity_ContractItemId",
                        column: x => x.ContractItemId,
                        principalTable: "tblContractItemEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblContractEntity_CustomerId",
                table: "tblContractEntity",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractEntity_PlotId",
                table: "tblContractEntity",
                column: "PlotId");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractEntity_ProjectId",
                table: "tblContractEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractItemEntity_ContractId",
                table: "tblContractItemEntity",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_tblContractPaidItemEntity_ContractItemId",
                table: "tblContractPaidItemEntity",
                column: "ContractItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPhaseEntity_ProjectId",
                table: "tblPhaseEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPlotEntity_ProjectId",
                table: "tblPlotEntity",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblContractPaidItemEntity");

            migrationBuilder.DropTable(
                name: "tblPhaseEntity");

            migrationBuilder.DropTable(
                name: "tblContractItemEntity");

            migrationBuilder.DropTable(
                name: "tblContractEntity");

            migrationBuilder.DropTable(
                name: "tblCustomerEntity");

            migrationBuilder.DropTable(
                name: "tblPlotEntity");

            migrationBuilder.DropTable(
                name: "tblLandProjectEntity");
        }
    }
}
