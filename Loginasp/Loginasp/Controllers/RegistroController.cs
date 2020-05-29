using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Loginasp.Models;
using Loginasp.Util;

namespace Loginasp.Controllers
{
    public class RegistroController : Controller
    {

        public ActionResult Registro()
        {

            return View("vistaRegistro");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(User nuevoUsuario, FormCollection datosObtenidos, HttpPostedFileBase image)
        {
            Usuario usuarioEntrante = new Usuario();
            string password = datosObtenidos["password"];
            string passwordVerificacion = datosObtenidos["passwordVerificacion"];

            // Verifica si las contraseñas son distintas
            if (!(password == passwordVerificacion))
            {
                Session["Contraseña"] = "Las contraseñas no coinciden";
                return RedirectToAction("Registro");
            }



            var imageName = string.Empty;
            var path = string.Empty;

            //Verifica que la imagen su contenido en byte sea mayor a 0
            if (image.ContentLength > 0)
            {
                imageName = Path.GetFileName(image.FileName);
                path = Path.Combine(Server.MapPath("~/assets/fotos"), imageName);
                image.SaveAs(path);
                nuevoUsuario.foto_path = ("/assets/fotos/" + imageName);
            }

            // Crea la 'Sal' para la contraseña
            nuevoUsuario.helper = usuarioEntrante.crearHelper();
            // Crea el hash con la contraseña y con la sal
            nuevoUsuario.hash = usuarioEntrante.crearHash(password, nuevoUsuario.helper);



            if (!ModelState.IsValid)
            {
                return RedirectToAction("Registro");
            }


            try
            {

                using (UsuariosDBEntities db = new UsuariosDBEntities())
                {
                    db.User.Add(nuevoUsuario);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
        
              
            }

            catch (Exception ex)
            {
          
                ModelState.AddModelError("Error al agregar usuario", ex);
                return RedirectToAction("Registro");
            }
        }
            
    }

}
