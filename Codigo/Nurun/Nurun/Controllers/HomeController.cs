using Nurun.Data;
using Nurun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuarios objUser)
        {
            if (objUser.ValidLogin())
            {
                UsuariosModel model = new UsuariosModel();
                var obj = model.iniciarSesion(objUser);
                
                if (obj != null)
                {
                    Session["UserID"] = obj.IdUsuario.ToString();
                    Session["UserName"] = obj.Usuario.ToString();
                    Session["RolID"] = obj.IdRol.ToString();
                    if(obj.IdRol == 1)
                        return RedirectToAction("Visitas", "User");
                    else if (obj.IdRol == 2)
                        return RedirectToAction("ActivateUsers", "Admin");
                }
                else 
                {
                    Response.Write("<script>alert('No se pudo iniciar sesión con el usuario, verifique sus datos.');</script>");
                }
                

            }
            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CloseSession()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
 
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(Usuarios objUser)
        {
            if (ModelState.IsValid)
            {
                UsuariosModel model = new UsuariosModel();
                var result = model.crearUsuario(objUser);

                if (result.Resultado)
                {
                    Response.Write("<script>alert('Usuario creado con éxito, el administrador debe validar la cuenta para que pueda acceder al sistema.');</script>");
                    return RedirectToAction("Login");
                }
                else
                {
                    if(string.IsNullOrEmpty(result.Mensaje))
                        Response.Write("<script>alert('No se pudo crear el usuario, intente nuevamente.');</script>");
                    else
                        Response.Write(string.Format("<script>alert('Error al crear el usuario: {0}');</script>", result.Mensaje));
                }


            }
            return View(objUser);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private bool isNotLoged()
        {
            return object.Equals(null, Session["UserID"]);
        }
    }
}