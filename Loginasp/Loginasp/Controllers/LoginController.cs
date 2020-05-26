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


        // POST: Login/Create
        [HttpPost]
        public ActionResult Index(FormCollection datosObtenidos)
        {
            try
            {

                string usuario = datosObtenidos["InputNombre"];
                string password = datosObtenidos["InputPassword"];

                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.nombreUsuario = usuario;
                nuevoUsuario.password = password;

                if (nuevoUsuario.verificarUsuario())
                {
                    Storage.Instance.listadoUsuarios.Add(nuevoUsuario);
                    Session["mensaje"] = "Usuario Registrado";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Session["mensajeError"] = "El usuario ya existe";
                    return RedirectToAction("Index");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
