using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDEmpleados.Model.DAL;
using CRUDEmpleados.Model.Entities;
using CRUDEmpleados.Model.Abstract;

namespace CrudEmpleados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET: api/Empleados
        [HttpGet]
        public async Task<IEnumerable<Empleado>> GetEmpleados()
        {
            return await _empleadoService.ObtenerListaTodosEmpleados();
        }

        // GET: api/Empleados/ObtenerListaEmpleadosAscendente
        [HttpGet]
        [Route("ObtenerListaEmpleadosAscendente")]
        public async Task<IEnumerable<Empleado>> GetEmpleadosAsc()
        {
            return await _empleadoService.ObtenerListaTodosEmpleadosAsc();
        }



        // GET: api/Empleados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetEmpleado(int id)
        {
            var empleado = await _empleadoService.ObtenerEmpleadoPorId(id);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }

        // GET: api/Empleados/5
        [HttpGet]
        [Route("ObtenerEmpleadoPorNombre/{nombre}")]
        public async Task<ActionResult<Empleado>> GetEmpleadoByName(String nombre)
        {
            var empleado = await _empleadoService.ObtenerEmpleadoPorNombre(nombre);

            if (empleado == null)
            {
                return NotFound();
            }

            return empleado;
        }


        // PUT: api/Empleados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmpleado(int id, Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return BadRequest();
            }

            await _empleadoService.EditarEmpleado(empleado);


            return Ok(await _empleadoService.ObtenerEmpleadoPorId(id));
        }
        
        // POST: api/Empleados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Empleado>> PostEmpleado(Empleado empleado)
        {
            await _empleadoService.GuardarEmpleado(empleado);

            return CreatedAtAction("GetEmpleado", new { id = empleado.EmpleadoId }, empleado);
        }

        
        // DELETE: api/Empleados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpleado(int id)
        {
            var empleado = await _empleadoService.ObtenerEmpleadoPorId(id);
            if (empleado == null)
            {
                return NotFound();
            }

            await _empleadoService.EliminarEmpleado(id);

            return Ok(await _empleadoService.ObtenerEmpleadoPorId(id));
        }     
        
    }
}
