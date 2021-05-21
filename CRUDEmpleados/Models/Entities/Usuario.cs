using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
    }
}
