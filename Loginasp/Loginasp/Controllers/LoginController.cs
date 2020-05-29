using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loginasp.Models;
using Loginasp.Util;

namespace Loginasp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        // post: Login
        [HttpPost]
        public ActionResult Index(FormCollection datosLogin)
        {
            string correoObtenido = datosLogin["InputCorreo"];
            string passwordObtenida = datosLogin["InputPassword"];

            try
            {
                using (UsuariosDBEntities db = new UsuariosDBEntities())
                {

                    User usuarioEncontrado = new User();

                    var query = from user in db.User where user.correo == correoObtenido select user;

                    usuarioEncontrado = query.FirstOrDefault<User>();

                    if (usuarioEncontrado == null)
                    {
                        return RedirectToAction("Index");
                    }

          
                    Usuario usuarioExtra = new Usuario();

                    if (!usuarioExtra.validarUsuario(usuarioEncontrado, passwordObtenida))
                    {
                        return RedirectToAction("Index");
                    }

                    Storage.Instance.usuarioLogueado = usuarioEncontrado;

                    return RedirectToAction("Index", "Usuarios");

                }
            }
            catch (Exception ex)
            {

                return View();
            }

        }

        //Get
        public ActionResult CerrarSesion()
        {
            Storage.Instance.usuarioLogueado = null;

            return RedirectToAction("Index", "Login");
        }

    }
}