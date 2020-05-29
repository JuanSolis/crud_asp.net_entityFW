using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Loginasp.Models;

namespace Loginasp.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public ActionResult Index()
        {

            List<User> listaUsuarios = new List<User>();
            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                listaUsuarios = db.User.ToList();
            }

            return View(listaUsuarios);
        }

        public ActionResult Perfil(int id)
        {
            User usuarioAEditar = new User();

            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {   

                var query = from user in db.User where user.id == id select user;

                usuarioAEditar = query.FirstOrDefault<User>();
            }
            
            return View(usuarioAEditar);
        }

        [HttpPost]
        public ActionResult Perfil(User usuarioActualizado, HttpPostedFileBase image, int id)
        {
            User usuarioEncontrado = new User();
            var imageName = string.Empty;
            var path = string.Empty;



            using (UsuariosDBEntities db = new UsuariosDBEntities())
            {
                
                var query = from user in db.User where user.id == id select user;

                usuarioEncontrado = query.FirstOrDefault<User>();

                //Verifica que la imagen su contenido en byte sea mayor a 0
                if (image.ContentLength > 0)
                {
                    imageName = Path.GetFileName(image.FileName);
                    path = Path.Combine(Server.MapPath("~/assets/fotos"), imageName);
                    image.SaveAs(path);
                    usuarioEncontrado.foto_path = ("/assets/fotos/" + imageName);
                }
                try
                    {

                        usuarioEncontrado.correo = usuarioActualizado.correo;
                        usuarioEncontrado.direccion = usuarioActualizado.correo;
                        usuarioEncontrado.edad = usuarioActualizado.edad;
                        usuarioEncontrado.nombre_completo = usuarioActualizado.nombre_completo;
                        usuarioEncontrado.pais = usuarioActualizado.pais;
                        usuarioEncontrado.direccion = usuarioActualizado.direccion;
                        db.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    catch (Exception ex)
                    {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("Error al agregar usuario", ex);
                    return RedirectToAction("Perfil", id);

                    }
            }
            
        }
    }
}