using CRUDEmpleados.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Model.Abstract
{
    public interface ICargoService
    {
        Task<IEnumerable<Cargo>> ObtenerListaCargos();
    }
}
