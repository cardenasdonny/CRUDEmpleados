using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpleados.Model.Entities
{
    public class UsuarioIdentity:IdentityUser
    {
        [Required(ErrorMessage ="El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public int Rol { get; set; }
    }
}
