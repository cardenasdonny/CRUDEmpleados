using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Model.Entities
{
    public class Cargo
    {
        [Key]
        public int CargoId { get; set; }
        public string Nombre { get; set; }
    }
}
