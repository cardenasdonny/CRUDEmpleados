using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpleados.Business.Dtos.Usuarios
{
    public class ResetearPasswordDto
    {
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El password es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password",
            ErrorMessage = "El Password y confirmar password debe coincidir")]
        public string ConfirmarPassword { get; set; }

        public string Token { get; set; }
    }
}
