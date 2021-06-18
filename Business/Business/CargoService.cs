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

        public async Task<IEnumerable<Cargo>> ObtenerListaCargos()
        {
            return await _context.Cargos.ToListAsync();
        }

    }
}
