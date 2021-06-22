using CRUDEmpleados.Model.Entities;
using CrudEmpleados.Web.ViewModels.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudEmpleados.Model.Entities;

namespace CrudEmpleados.Web.Controllers
{
    
    public class UsuariosController : Controller
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        const string SesionNombre = "_Nombre";
   


        public UsuariosController(UserManager<UsuarioIdentity> userManager, SignInManager<UsuarioIdentity> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var listaUsuarios = await _userManager.Users.ToListAsync();
            return View(listaUsuarios);
        }

        public IActionResult Crear() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                UsuarioIdentity usuarioIdentity = new()
                {
                    UserName = usuarioViewModel.Email,
                    Email = usuarioViewModel.Email,
                    Nombre = usuarioViewModel.Nombre,
                    Rol = usuarioViewModel.Rol
                };

                try
                {
                    //guardar el usuario
                    var resultado = await _userManager.CreateAsync(usuarioIdentity, usuarioViewModel.Password);
                    if (resultado.Succeeded)
                        return RedirectToAction("Index");
                    else
                        return View(usuarioViewModel);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return View();
        }

        //Eliminar usuario

        [HttpPost]
        public async Task<IActionResult> Eliminar(string id) 
        {
            //buscamos el usuario
            var usuario = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(usuario);
            return RedirectToAction("Index");

        }

        //****************login*******

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RecordarMe, false);

                if (result.Succeeded)
                {
                    //buscamos el usuario
                    var usuario = await _userManager.FindByEmailAsync(loginViewModel.Email);                   

                    _httpContextAccessor.HttpContext.Session.SetString(SesionNombre, usuario.Nombre);
                    return RedirectToAction("Dashboard", "Admin");
                }

                return View();
            }
            else
            {
                return View(loginViewModel);
            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }

    }
}
