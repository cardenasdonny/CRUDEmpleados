using CrudEmpleados.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDEmpleados.Model.Entities
{
    public class Empleado
    {
        [Key]
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

        public string RutaImagen { get; set; }

        public virtual List<EmpleadoDetalle> EmpleadoDetalle { get; set; }

    }
}
