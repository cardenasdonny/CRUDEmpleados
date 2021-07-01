using CRUDEmpleados.Model.Abstract;
using CRUDEmpleados.Model.Entities;
using CrudEmpleados.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrudEmpleados.Business.Dtos;

namespace CRUDEmpleados.Web.Controllers
{
    
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

        //[Authorize(Roles ="Administrador")]

        [HttpGet]
        public async Task<IActionResult> CrearEditarEmpleado(int id=0)
        {

            if (id == 0)
            {
                EmpleadoDto empleadoDto = new();
                empleadoDto.Cargos = _cargoService.ObtenerListaCargos();
                
                /*
                listaCargos.ForEach(c =>
                {
                    Cargo cargo = new()
                    {
                        CargoId = c.CargoId,
                        Nombre = c.Nombre
                    };

                });
                */

                return View(empleadoDto);
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












            /*
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
            */
        }

        [HttpPost]
        public async Task<IActionResult> CrearEditarEmpleado(int? id, EmpleadoDto EmpleadoDto)
        {
            //preguntamos si el modelo es válido o no (comprueba validaciones)
            if (ModelState.IsValid)
            {
                // se crea un objeto de tipo empleado y se le asignan las propiedades que vienen de empleadoViewModel
                Empleado empleado = new()
                {
                    Nombre = EmpleadoDto.Nombre,           
                    Documento = EmpleadoDto.Documento,
                    EmpleadoId = EmpleadoDto.EmpleadoId,
                    Telefono = EmpleadoDto.Telefono,
                    Estado = EmpleadoDto.Estado

                };

                string path = null;
                string wwwRootPath = null;

                // si se utiliza una imagen entonces
                if (EmpleadoDto.Imagen != null) { 
                    //obtenemos la ruta raiz de nuestro proyecto
                    wwwRootPath = _hostEnvironment.WebRootPath;
                    //obtenemos el nombre de la imagen
                    string nombreImagen = Path.GetFileNameWithoutExtension(EmpleadoDto.Imagen.FileName);
                    //obtenemos la extensión de la imagen .jpg - .png etc
                    string extension = Path.GetExtension(EmpleadoDto.Imagen.FileName);
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
                            await EmpleadoDto.Imagen.CopyToAsync(fileStream);
                        }                       

                        empleado.Estado = true;
                       
                        await _empleadoService.GuardarEmpleado(empleado);

                        await _empleadoService.GuardarEmpleadoDetalleCargo(EmpleadoDto.Cargos);

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
                            await EmpleadoDto.Imagen.CopyToAsync(fileStream);

                            //borramos la foto vieja
                            if (EmpleadoDto.RutaImagen!=null)
                            {
                                FileInfo file = new FileInfo(wwwRootPath + "/imagenes/" + EmpleadoDto.RutaImagen);
                                file.Delete();
                            }
                           

                        }
                        else
                        {
                            empleado.RutaImagen = EmpleadoDto.RutaImagen;

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
                return View(EmpleadoDto);
            }          

        }

        [HttpGet]
        public async Task<IActionResult> DetalleEmpleado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _empleadoService.ObtenerEmpleadoPorId(id.Value);
            EmpleadoDto empleadoDto = new()
            {
                EmpleadoId = empleado.EmpleadoId,
                Nombre = empleado.Nombre,
                Documento = empleado.Documento,
                Estado = empleado.Estado,
                RutaImagen = empleado.RutaImagen,
                Telefono = empleado.Telefono
            };

            empleadoDto.Cargos = _cargoService.ObtenerListaEmpleadoCargos(id.Value);

            return View(empleadoDto);
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
