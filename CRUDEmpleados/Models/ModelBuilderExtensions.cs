using CRUDEmpleados.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>().HasData(
                new Cargo
                {
                    CargoId = 1,
                    Nombre = "Secretaria"
                },
                new Cargo
                {
                    CargoId = 2,
                    Nombre = "Gerente"
                },
                new Cargo
                {
                    CargoId = 3,
                    Nombre = "Contador"
                }
            );

            modelBuilder.Entity<Empleado>().HasData(
                new Empleado
                {
                    EmpleadoId = 1,
                    CargoId = 1,
                    Documento = 661122,
                    Estado = true,
                    Nombre = "Luisa",
                    Telefono = "554433",                   
                },
                new Empleado
                {
                    EmpleadoId = 2,
                    CargoId = 3,
                    Documento = 12345,
                    Estado = true,
                    Nombre = "Juan",
                    Telefono = "445566",
                },
                new Empleado
                {
                    EmpleadoId = 3,
                    CargoId = 3,
                    Documento = 229911,
                    Estado = true,
                    Nombre = "Daniel",
                    Telefono = "885566",
                }
            );

        }
    }
}
