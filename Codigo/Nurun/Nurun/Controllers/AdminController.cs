using Nurun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult ActivateUsers(int? id)
        {
            if (id != null)
                return RedirectToAction("Edit", new { id = id });
            else
            {
                UsuariosModel usuario = new UsuariosModel();
                return View(usuario.obtenerUsuarios());
            }
        }
     
        public ActionResult Edit(int id)
        {
            UsuariosModel model = new UsuariosModel();
            var result = model.activarUsuario(id);

            if (!result.Resultado)
            {                   
                Response.Write(string.Format("<script>alert('Ha ocurrido un error al intentar activar el usuario: {0}.');</script>", result.Mensaje));
            }

            return RedirectToAction("ActivateUsers");
        }

        public ActionResult Hospitals()
        {
            HospitalesModel model = new HospitalesModel();
            return View(model.obtenerHospitales());
        }

        public ActionResult CreateHospital()
        {
            return View();
        }

        public ActionResult Doctors()
        {
            DoctorsModel model = new DoctorsModel();
            return View(model.obtenerHospitales());
        }
    }
}