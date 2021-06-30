using CrudEmpleados.Business.Dtos;
using CrudEmpleados.Model.Entities;
using CRUDEmpleados.Model.Abstract;
using CRUDEmpleados.Model.DAL;
using CRUDEmpleados.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Model.Business
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
            return await _context.Empleados.OrderByDescending(i=>i.EmpleadoId).ToListAsync();
        }

        public async Task<IEnumerable<Empleado>> ObtenerListaTodosEmpleadosAsc()
        {
            return await _context.Empleados.OrderBy(i => i.EmpleadoId).ToListAsync();
        }

        public async Task<Empleado> ObtenerEmpleadoPorNombre(string nombre)
        {
            //return await _context.Empleados.Include(x => x.Cargo).FindAsync(id);
            return await _context.Empleados.FirstOrDefaultAsync(x => x.Nombre.Equals(nombre));
        }

        public async Task<Empleado> ObtenerEmpleadoPorId(int id)
        {
            //return await _context.Empleados.Include(x => x.Cargo).FindAsync(id);
            return await _context.Empleados.FirstOrDefaultAsync(x=>x.EmpleadoId == id);
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

        public int ObtenerEmpleadoMaxId()
        {
            return _context.Empleados.Max(e => e.EmpleadoId);
        }




        public async Task GuardarEmpleadoDetalleCargo(List<CargoDto> listacargoDtos)
        {            

            foreach (var cargoDto in listacargoDtos)
            {
                int maxId = ObtenerEmpleadoMaxId();
                if (cargoDto.Seleccionado == true)
                {
                    EmpleadoCargos empleadoCargos = new()
                    {
                        CargoId = cargoDto.CargoId,
                        EmpleadoId = maxId
                    };
                    await GuardarEmpleadoDetalleCargo(empleadoCargos);

                }
                
            }
            
            
        }

        public async Task GuardarEmpleadoDetalleCargo(EmpleadoCargos empleadoCargos)
        {
            _context.Add(empleadoCargos);
            await _context.SaveChangesAsync();
        }


        }
}
