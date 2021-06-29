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
using CrudEmpleados.Business.Dtos.Usuarios;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CrudEmpleados.Web.Controllers
{
    
    public class UsuariosController : Controller
    {
        private readonly UserManager<UsuarioIdentity> _userManager;
        private readonly SignInManager<UsuarioIdentity> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        const string SesionNombre = "_Nombre";
   


        public UsuariosController(UserManager<UsuarioIdentity> userManager, SignInManager<UsuarioIdentity> signInManager, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
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


        [HttpGet]
        public IActionResult OlvidePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OlvidePassword(OlvidePasswordDto olvidePasswordDto)
        {
            if (ModelState.IsValid)
            {
                // buscamos el email a ver si existe
                var usuario = await _userManager.FindByEmailAsync(olvidePasswordDto.Email);

                if (usuario != null)
                {
                    //generamos un token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);

                    //creamos un link para resetear el password
                    var passwordresetLink = Url.Action("ResetearPassword", "Usuarios",
                        new { email = olvidePasswordDto.Email, token = token }, Request.Scheme);

                    //Opción 1 en la que usamos smtp

                    /*
                    MailMessage mensaje = new();
                    mensaje.To.Add(olvidePasswordDto.Email); //destinatario
                    mensaje.Subject = "CrudEmpleados recuperar password";
                    mensaje.Body = passwordresetLink;
                    mensaje.IsBodyHtml = false;
                    //mensaje.From = new MailAddress("pruebas@xofsystems.com","Notificaciones");
                    mensaje.From = new MailAddress(_configuration["Mail"], "Notificaciones");
                    SmtpClient smtpClient = new("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_configuration["Mail"], "Tempo123!");
                    smtpClient.Send(mensaje);
                    return View("OlvidePasswordConfirmacion");
                    */

                    //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
                    //Opción 2 utilizando un API para reenvio de correo (SENDGRID)

                    var clienteCorreo = new SendGridClient("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");
                    var mensaje = new SendGridMessage();
                    mensaje.From = new EmailAddress("grcv.91@gmail.com", "Elguille");
                    mensaje.Subject = "Recuperar contraseña";
                    mensaje.AddTo(olvidePasswordDto.Email);
                    mensaje.PlainTextContent = "Funciona este correo";
                    mensaje.HtmlContent = "<strong>Funciona este correo</strong>";
                    mensaje.SetClickTracking(false, false);
                    var respuesta = await clienteCorreo.SendEmailAsync(mensaje);

                    if (respuesta.IsSuccessStatusCode == true)
                        return Ok();


                }
                else
                {
                    return View(olvidePasswordDto);
                }
            }          


            return View(olvidePasswordDto);
        }

        //Cuando hacemos clic en el link que llegó al correo
        [HttpGet]
        public IActionResult ResetearPassword(string token, string email)
        {
            if(token==null || email == null)
            {
                ModelState.AddModelError("", "Error token");
            }
            return View();
        }
        //Cuando hacemos clic en el link que llegó al correo
        [HttpPost]
        public async Task<IActionResult> ResetearPassword(ResetearPasswordDto resetearPasswordDto)
        {
            if (ModelState.IsValid)
            {
                //buscamos el usuario
                var usuario = await _userManager.FindByEmailAsync(resetearPasswordDto.Email);

                if (usuario != null)
                {
                    //se resetea el password
                    var result = await _userManager.ResetPasswordAsync(usuario, resetearPasswordDto.Token, resetearPasswordDto.Password);
                    if (result.Succeeded)
                        return View("ResetearPasswordConfirmacion");
                    else
                    {
                        foreach (var errores in result.Errors)
                        {
                            if(errores.Description.ToString().Equals("Invalid token."))
                            ModelState.AddModelError("", "El token es invalido");
                        }
                        return View(resetearPasswordDto);
                    }
                }
                return View(resetearPasswordDto);
            }
            return View(resetearPasswordDto);
        }


        public async Task<IActionResult> CerrarSesion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Usuarios");
        }

    }
}
