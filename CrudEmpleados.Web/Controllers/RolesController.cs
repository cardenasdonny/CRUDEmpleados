using CrudEmpleados.Model.Entities;
using CrudEmpleados.Web.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudEmpleados.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(UserManager<UsuarioIdentity> userManager, SignInManager<UsuarioIdentity> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var listaRoles = _roleManager.Roles.ToList();
            return View(listaRoles);
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(string rol)
        {
            //create new role using roleManager
            //return to displayRoles
            await _roleManager.CreateAsync(new IdentityRole(rol));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AsignarRolesUsuario()
        {
            var listaUsuarios = await _userManager.Users.ToListAsync();
            var listaRoles = await _roleManager.Roles.ToListAsync();

            ViewBag.Usuarios = new SelectList(listaUsuarios, "Id", "Nombre");
            ViewBag.Roles = new SelectList(listaRoles, "Name", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AsignarRolesUsuario(RolesUsuarioViewModel rolesUsuarioViewModel)
        {
            var usuario = await _userManager.FindByIdAsync(rolesUsuarioViewModel.UsuarioId);
            await _userManager.AddToRoleAsync(usuario, rolesUsuarioViewModel.NombreRol);

            return RedirectToAction("Index", "Usuarios");
        }


        [HttpGet]
        public async Task<IActionResult> Detalle(string usuarioId)
        {
            var usuario = await _userManager.FindByIdAsync(usuarioId);
            ViewBag.NombreUsuario = usuario.Nombre;
            ViewBag.UsuarioId = usuario.Id;
            var listaRolesUsuario = await _userManager.GetRolesAsync(usuario);

            return View(listaRolesUsuario);
        }

        [HttpGet]
        public async Task<IActionResult> EliminarRolUsuario(string rol, string usuarioId)
        {
            //get user from userName
            //remove role of user using userManager
            //return to details with parameter userId

            var user = await _userManager.FindByIdAsync(usuarioId);

            var result = await _userManager.RemoveFromRoleAsync(user, rol);

            return RedirectToAction(nameof(Detalle), new { UsuarioId = user.Id });
        }



        [HttpGet]
        public async Task<IActionResult> EliminarRol(string rol)
        {
            //get role to delete using role Name
            //delete role using roleManager
            //redirect to displayroles

            var roleToDelete = await _roleManager.FindByNameAsync(rol);
            var result = await _roleManager.DeleteAsync(roleToDelete);

            return RedirectToAction(nameof(Index));
        }




    }
}
