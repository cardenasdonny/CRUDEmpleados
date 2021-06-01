using CRUDEmpleados.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Models.Abstract
{
    public interface ICargoService
    {
        Task<IEnumerable<Cargo>> ObtenerListaCargos();
    }
}
