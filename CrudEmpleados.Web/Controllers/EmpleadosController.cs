using CRUDEmpleados.Model.Abstract;
using CRUDEmpleados.Model.Entities;
using CRUDEmpleados.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDEmpleados.Web.Controllers
{
    [Authorize]
    public class EmpleadosController : Controller
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ICargoService _cargoService;

        public EmpleadosController(IEmpleadoService empleadoService, IWebHostEnvironment hostEnvironment, ICargoService cargoService)
        {
            _empleadoService = empleadoService;
            _hostEnvironment = hostEnvironment;
            _cargoService = cargoService;
        }


        
        [AllowAnonymous]
        [HttpGet]
        public async Task <IActionResult> Index()
        {
          
            return View(await _empleadoService.ObtenerListaTodosEmpleados());
        }

        [HttpGet]
        public async Task<IActionResult> CrearEditarEmpleado(int id=0)
        {
            //ViewData["ListaCargos"] = new SelectList(await _cargoService.ObtenerListaCargos(), "CargoId", "Nombre");
            ViewBag.ListaCargos = new SelectList(await _cargoService.ObtenerListaCargos(), "CargoId", "Nombre");


            if (id == 0)
            {
                return View(new EmpleadoViewModel());
            }
            else
            {              

                Empleado empleado = await _empleadoService.ObtenerEmpleadoPorId(id);
                EmpleadoViewModel empleadoViewModel = new()
                {
                    Nombre = empleado.Nombre,
                    CargoId = empleado.CargoId,
                    Documento = empleado.Documento,
                    EmpleadoId = empleado.EmpleadoId,
                    Telefono = empleado.Telefono,
                    Estado = empleado.Estado,
                    RutaImagen = empleado.RutaImagen
                };
                return View(empleadoViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearEditarEmpleado(int? id, EmpleadoViewModel empleadoViewModel)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                // se crea un objeto de tipo empleado y se le asignan las propiedades que vienen de empleadoViewModel
                Empleado empleado = new()
                {
                    Nombre = empleadoViewModel.Nombre,
                    CargoId = empleadoViewModel.CargoId,
                    Documento = empleadoViewModel.Documento,
                    EmpleadoId = empleadoViewModel.EmpleadoId,
                    Telefono = empleadoViewModel.Telefono,
                    Estado = empleadoViewModel.Estado

                };

                string path = null;
                string wwwRootPath = null;

                // si se utiliza una imagen entonces
                if (empleadoViewModel.Imagen != null) { 
                    //obtenemos la ruta raiz de nuestro proyecto
                    wwwRootPath = _hostEnvironment.WebRootPath;
                    //obtenemos el nombre de la imagen
                    string nombreImagen = Path.GetFileNameWithoutExtension(empleadoViewModel.Imagen.FileName);
                    //obtenemos la extensión de la imagen .jpg - .png etc
                    string extension = Path.GetExtension(empleadoViewModel.Imagen.FileName);
                    //concatenamos el nombre de la imagen con el año-minuto-segundos-fraciones de segundo + la extensión
                    empleado.RutaImagen = nombreImagen + DateTime.Now.ToString("yymmssfff") + extension;
                    //Obetenemos la ruta en donde vamos a guardar la imagen
                    path = Path.Combine(wwwRootPath + "/imagenes/" + empleado.RutaImagen);
                }


                if (id == 0)
                {
                    try
                    {
                        // si se va a guardar una imagen
                        if (path != null)
                        {
                            //copiamos la imagen a la ruta especifica
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await empleadoViewModel.Imagen.CopyToAsync(fileStream);
                        }                       

                        empleado.Estado = true;
                       
                        await _empleadoService.GuardarEmpleado(empleado);
                        TempData["Accion"] = "GuardarEmpleado";
                        TempData["Mensaje"] = "Empleado guardado con éxito";
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Error realizando la operación";
                        return RedirectToAction("Index");
                    }
                } // si vamos a editar
                else
                {
                    if (id != empleado.EmpleadoId)
                    {
                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Error realizando la operación";
                        return RedirectToAction("Index");
                    }

                    try
                    { 

                        if (path != null)
                        {
                            //copiamos la imagen a la ruta especifica
                            using var fileStream = new FileStream(path, FileMode.Create);
                            await empleadoViewModel.Imagen.CopyToAsync(fileStream);

                            //borramos la foto vieja
                            if (empleadoViewModel.RutaImagen!=null)
                            {
                                FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + empleadoViewModel.RutaImagen);
                                file.Delete();
                            }
                           

                        }
                        else
                        {
                            empleado.RutaImagen = empleadoViewModel.RutaImagen;

                        }

                        

                        await _empleadoService.EditarEmpleado(empleado);
                        TempData["Accion"] = "EditarEmpleado";
                        TempData["Mensaje"] = "Empleado editado con éxito";
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {

                        TempData["Accion"] = "Error";
                        TempData["Mensaje"] = "Error realizando la operación";
                        return RedirectToAction("Index");
                    }

                }              
               
            }
            else
            {
                return View(empleadoViewModel);
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
