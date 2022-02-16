using Microsoft.EntityFrameworkCore.Migrations;

namespace Proiect.Migrations
{
    public partial class CarGadget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gadget",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GadgetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gadget", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CarGadget",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarID = table.Column<int>(nullable: false),
                    GadgetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGadget", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CarGadget_Car_CarID",
                        column: x => x.CarID,
                        principalTable: "Car",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarGadget_Gadget_GadgetID",
                        column: x => x.GadgetID,
                        principalTable: "Gadget",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarGadget_CarID",
                table: "CarGadget",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_CarGadget_GadgetID",
                table: "CarGadget",
                column: "GadgetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarGadget");

            migrationBuilder.DropTable(
                name: "Gadget");
        }
    }
}
