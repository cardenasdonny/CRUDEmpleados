using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Web.ViewModels
{
    public class EmpleadoViewModel
    {        
        public int EmpleadoId { get; set; }

        [DisplayName("Nombre completo")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Column("NombreEmpleado", TypeName = "nvarchar(50)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio")]
        [Range(1, 9999999999999, ErrorMessage = "El documento es requerido")]
        public int Documento { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "El cargo es obligatorio")]
        [Range(1, 10, ErrorMessage = "El cargo es requerido")]
        public int CargoId { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }
        
        public IFormFile Imagen { get; set; }

        public string RutaImagen { get; set; }

    }
}
