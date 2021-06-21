using CrudEmpleados.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
