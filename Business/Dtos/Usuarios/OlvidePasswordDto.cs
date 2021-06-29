using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpleados.Business.Dtos.Usuarios
{
    public class OlvidePasswordDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }
    }
}
