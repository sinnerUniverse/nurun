using Nurun.Data;
using Nurun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Visitas()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            VisitasMedicasModel model = new VisitasMedicasModel();
            var idUsuario = int.Parse(Session["UserID"].ToString());
            var noTieneMedico = object.Equals(null, Session["MedicoID"]);
            var visitas = model.obtenerVisitas(idUsuario);
            if(noTieneMedico)
                Response.Write("<script>alert('No cuenta con un médico asignado, verifique con su administrador para que le asigne uno y pueda agendar una visita médica.');</script>");
            return View(visitas);
        }

        public ActionResult CreateVisit()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVisit(VisitaMedica objVis)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                VisitasMedicasModel model = new VisitasMedicasModel();
                var idUsuario = int.Parse(Session["UserID"].ToString());
                var result = model.registrarVisita(objVis, idUsuario);

                if (result.Resultado)
                {
                    Response.Write("<script>alert('Visita registrada con éxito.');</script>");
                    return RedirectToAction("Visitas");
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Mensaje))
                        Response.Write("<script>alert('No se pudo registrar la visita, intente nuevamente.');</script>");
                    else
                        Response.Write(string.Format("<script>alert('Error al registrar la visita: {0}');</script>", result.Mensaje));
                }


            }
            return View(objVis);
        }

        private bool isNotLoged()
        {
            return object.Equals(null, Session["UserID"]);
        }
    }
}