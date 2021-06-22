using CRUDEmpleados.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpleados.Model.Entities
{
    public class EmpleadoDetalle
    {
        [Key]
        public int EmpleadoDetalleId { get; set; }

        [ForeignKey("EmpleadoId")]
        public int EmpleadoId { get; set; }

        [ForeignKey("CargoId")]
        public int CargoId { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}
