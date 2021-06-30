using CRUDEmpleados.Model.Entities;
using CRUDEmpleados.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CrudEmpleados.Model.Entities;

namespace CRUDEmpleados.Model.DAL
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);     
            modelBuilder.Seed();
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<UsuarioIdentity> Usuarios { get; set; }

        public DbSet<EmpleadoCargos> EmpleadoCargos { get; set; }

    }
}
