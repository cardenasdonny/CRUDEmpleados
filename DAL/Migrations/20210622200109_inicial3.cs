using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudEmpleados.Model.Migrations
{
    public partial class inicial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_Cargos_CargoId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_CargoId",
                table: "Empleados");

            migrationBuilder.CreateTable(
                name: "EmpleadoDetalles",
                columns: table => new
                {
                    EmpleadoDetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoDetalles", x => x.EmpleadoDetalleId);
                    table.ForeignKey(
                        name: "FK_EmpleadoDetalles_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoDetalles_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoDetalles_CargoId",
                table: "EmpleadoDetalles",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoDetalles_EmpleadoId",
                table: "EmpleadoDetalles",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleadoDetalles");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_CargoId",
                table: "Empleados",
                column: "CargoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_Cargos_CargoId",
                table: "Empleados",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
