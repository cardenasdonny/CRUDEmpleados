using CrudEmpleados.Business.Dtos;
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
    public class CargoService:ICargoService
    {
        private readonly AppDbContext _context;
        public CargoService(AppDbContext context)
        {
            _context = context;
        }
        /*
        public async Task<List<Cargo>> ObtenerListaCargos()
        {            
            return await _context.Cargos.ToListAsync();
        }
        */
        public List<CargoDto> ObtenerListaCargos()
        {
            List<CargoDto> listaCargoDtos = new();
            _context.Cargos.ToList().ForEach(c =>
            {
                CargoDto cargoDto = new()
                {
                    CargoId = c.CargoId,
                    Nombre = c.Nombre
                };
                listaCargoDtos.Add(cargoDto);
            });
            return listaCargoDtos;
        }





        public List<CargoDto> ObtenerListaEmpleadoCargos(int id)
        {
            List<CargoDto> listaEmpleadoCargoDtos = new();
            _context.EmpleadoCargos.Include(x => x.Cargo).Where(x => x.EmpleadoId == id).ToList().ForEach(d =>
                {
                    CargoDto empleadoCargoDto = new()
                    {
                        CargoId = d.CargoId,
                        Nombre = d.Cargo.Nombre
                    };
                    listaEmpleadoCargoDtos.Add(empleadoCargoDto);

                }
            );
            
            return listaEmpleadoCargoDtos;
        }

    }
}
