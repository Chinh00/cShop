using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShipperInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipperOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipperOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShipperInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipperOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipperOrders_ShipperInfos_ShipperInfoId",
                        column: x => x.ShipperInfoId,
                        principalTable: "ShipperInfos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipperInfos_Id",
                table: "ShipperInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperOrders_Id",
                table: "ShipperOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShipperOrders_ShipperInfoId",
                table: "ShipperOrders",
                column: "ShipperInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipperOrders");

            migrationBuilder.DropTable(
                name: "ShipperInfos");
        }
    }
}
