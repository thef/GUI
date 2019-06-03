using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Data.Migrations
{
    public partial class CreateSensor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WoodArt = table.Column<string>(nullable: true),
                    SensorId = table.Column<string>(nullable: true),
                    GpsCoordinateLat = table.Column<double>(nullable: false),
                    GpsCoordinateLon = table.Column<double>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_LocationId",
                table: "Sensors",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sensors");
        }
    }
}
