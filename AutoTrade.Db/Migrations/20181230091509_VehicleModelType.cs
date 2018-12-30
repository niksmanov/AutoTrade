using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoTrade.Db.Migrations
{
    public partial class VehicleModelType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleType",
                table: "VehicleModels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "VehicleModels");
        }
    }
}
