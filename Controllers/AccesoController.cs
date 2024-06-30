using Microsoft.AspNetCore.Mvc;
using TPAlumnotest.Data;
using TPAlumnotest.Models;
using Microsoft.EntityFrameworkCore;
using TPAlumnotest.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace TPAlumnotest.Controllers
{
    public class AccesoController : Controller
    {
        private readonly AppDBContext _dbContext;

        public AccesoController(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            if (User.Identity!.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>  Registrar(UsuarioVM modelo)
        {
            if(modelo.Clave != modelo.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
            }
            else
            {
            Usuario usuario = new Usuario()
            {
                Username = modelo.Username,
                Clave = modelo.Clave,
            };
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            if(usuario.IDUsuario != 0)
            {
                return RedirectToAction("Login", "Acceso");
            }
            ViewData["Mensaje"] = "No se pudo crear el usuario";

             
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated) { return RedirectToAction("Index", "Home"); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM modelo)
        {
            Usuario? exist_usuario = await _dbContext.Usuarios
                .Where(u =>
                u.Username == modelo.Username &&
                u.Clave == modelo.Clave
                ).FirstOrDefaultAsync();
            if (exist_usuario == null)
            {
                ViewData["Mensaje"] = "Error en nombre de usuario o contraseña";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, exist_usuario.Username),
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");
        }
    }
}
