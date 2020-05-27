using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult Registro(User nuevoUsuario)
        {
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
                ex.Message.ToString();
                return RedirectToAction("Registro");
            }
        }
            
    }

}
