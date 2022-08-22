using Microsoft.EntityFrameworkCore.Migrations;

namespace eczanesepeti2.Data.Migrations
{
    public partial class Tablolar4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IlId",
                table: "Ilce",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Il",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Il", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ilce_IlId",
                table: "Ilce",
                column: "IlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ilce_Il_IlId",
                table: "Ilce",
                column: "IlId",
                principalTable: "Il",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ilce_Il_IlId",
                table: "Ilce");

            migrationBuilder.DropTable(
                name: "Il");

            migrationBuilder.DropIndex(
                name: "IX_Ilce_IlId",
                table: "Ilce");

            migrationBuilder.DropColumn(
                name: "IlId",
                table: "Ilce");
        }
    }
}
