using CrudEmpleados.Business.Dtos;
using CRUDEmpleados.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Model.Abstract
{
    public interface ICargoService
    {
        List<CargoDto> ObtenerListaCargos();
        List<CargoDto> ObtenerListaEmpleadoCargos(int id);


    }

}
