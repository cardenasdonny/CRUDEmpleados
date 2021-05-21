using CRUDEmpleados.Models.Abstract;
using CRUDEmpleados.Models.DAL;
using CRUDEmpleados.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Models.Business
{
    public class EmpleadoService: IEmpleadoService
    {
        private readonly AppDbContext _context;

        public EmpleadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> ObtenerListaTodosEmpleados()
        {
            return await _context.Empleados.Include(x=>x.Cargo).OrderByDescending(i=>i.EmpleadoId).ToListAsync();
        }

        public async Task<Empleado> ObtenerEmpleadoPorId(int id)
        {
            //return await _context.Empleados.Include(x => x.Cargo).FindAsync(id);
            return await _context.Empleados.Include(x => x.Cargo).FirstOrDefaultAsync(x=>x.EmpleadoId == id);
        }

        public async Task GuardarEmpleado(Empleado empleado)
        {
            _context.Add(empleado);
            await _context.SaveChangesAsync();
        }

        public async Task EditarEmpleado(Empleado empleado)
        {
            _context.Update(empleado);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarEmpleado(int id)
        {
            var empleado = await ObtenerEmpleadoPorId(id);
            _context.Remove(empleado);
            await _context.SaveChangesAsync();
        }

    }
}
