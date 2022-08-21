using Microsoft.EntityFrameworkCore.Migrations;

namespace eczanesepeti2.Data.Migrations
{
    public partial class Tablolar2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ilce",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAd = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilce", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eczane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EczaneAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IlceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eczane", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eczane_Ilce_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IlacEczane",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlacId = table.Column<int>(type: "int", nullable: false),
                    EczaneId = table.Column<int>(type: "int", nullable: false),
                    Sira = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IlacEczane", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IlacEczane_Eczane_EczaneId",
                        column: x => x.EczaneId,
                        principalTable: "Eczane",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IlacEczane_Ilac_IlacId",
                        column: x => x.IlacId,
                        principalTable: "Ilac",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eczane_IlceId",
                table: "Eczane",
                column: "IlceId");

            migrationBuilder.CreateIndex(
                name: "IX_IlacEczane_EczaneId",
                table: "IlacEczane",
                column: "EczaneId");

            migrationBuilder.CreateIndex(
                name: "IX_IlacEczane_IlacId",
                table: "IlacEczane",
                column: "IlacId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IlacEczane");

            migrationBuilder.DropTable(
                name: "Eczane");

            migrationBuilder.DropTable(
                name: "Ilce");
        }
    }
}
