﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudEmpleados.Business.Dtos
{
    public class EmpleadoDto
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
        public List<CargoDto> Cargos { get; set; }

        public string Telefono { get; set; }

        public bool Estado { get; set; }

        public IFormFile Imagen { get; set; }

        public string RutaImagen { get; set; }
    }

    public class CargoDto
    {
        public int CargoId { get; set; }
        public string Nombre { get; set; }
        public bool Seleccionado { get; set; }
    }
}
