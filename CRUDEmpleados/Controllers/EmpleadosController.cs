using CRUDEmpleados.Models.Abstract;
using CRUDEmpleados.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadosController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var lista = await _empleadoService.ObtenerListaTodosEmpleados();
            return View(await _empleadoService.ObtenerListaTodosEmpleados());
        }

        [HttpGet]
        public async Task<IActionResult> CrearEditarEmpleado(int id=0)
        {
            if (id == 0)
            {
                return View(new Empleado());
            }
            else
            {                
                return View(await _empleadoService.ObtenerEmpleadoPorId(id));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearEditarEmpleado(int? id, Empleado empleado)
        {            

            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    try
                    {
                        empleado.Estado = true;
                        await _empleadoService.GuardarEmpleado(empleado);
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    if (id != empleado.EmpleadoId)
                    {
                        return NotFound();
                    }

                    try
                    {
                        await _empleadoService.EditarEmpleado(empleado);
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                }              
               
            }
            else
            {
                return View(empleado);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> DetalleEmpleado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _empleadoService.ObtenerEmpleadoPorId(id.Value));
        }
                

        [HttpPost]     
        public async Task<IActionResult> EliminarEmpleado(int id)
        {
            try
            {
                await _empleadoService.EliminarEmpleado(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
