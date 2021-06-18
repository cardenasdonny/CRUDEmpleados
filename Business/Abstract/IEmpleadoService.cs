using CRUDEmpleados.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Model.Abstract
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<Empleado>> ObtenerListaTodosEmpleados();
        Task GuardarEmpleado(Empleado empleado);
        Task<Empleado> ObtenerEmpleadoPorId(int id);
        Task EditarEmpleado(Empleado empleado);
        Task EliminarEmpleado(int id);
    }
}
