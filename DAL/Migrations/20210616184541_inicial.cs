using Microsoft.EntityFrameworkCore.Migrations;

namespace CrudEmpleados.Model.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    CargoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.CargoId);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Documento = table.Column<int>(type: "int", nullable: false),
                    CargoId = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    RutaImagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_Empleados_Cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargos",
                        principalColumn: "CargoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "CargoId", "Nombre" },
                values: new object[] { 1, "Secretaria" });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "CargoId", "Nombre" },
                values: new object[] { 2, "Gerente" });

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "CargoId", "Nombre" },
                values: new object[] { 3, "Contador" });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "EmpleadoId", "CargoId", "Documento", "Estado", "NombreEmpleado", "RutaImagen", "Telefono" },
                values: new object[] { 1, 1, 661122, true, "Luisa", null, "554433" });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "EmpleadoId", "CargoId", "Documento", "Estado", "NombreEmpleado", "RutaImagen", "Telefono" },
                values: new object[] { 2, 3, 12345, true, "Juan", null, "445566" });

            migrationBuilder.InsertData(
                table: "Empleados",
                columns: new[] { "EmpleadoId", "CargoId", "Documento", "Estado", "NombreEmpleado", "RutaImagen", "Telefono" },
                values: new object[] { 3, 3, 229911, true, "Daniel", null, "885566" });

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_CargoId",
                table: "Empleados",
                column: "CargoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Cargos");
        }
    }
}
