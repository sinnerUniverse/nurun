using Nurun.Data;
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
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

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
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

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
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            HospitalesModel model = new HospitalesModel();
            return View(model.obtenerHospitales());
        }

        public ActionResult CreateHospital()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHospital(Hospitales objHosp)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                HospitalesModel model = new HospitalesModel();
                var result = model.crearHospital(objHosp);

                if (result.Resultado)
                {
                    Response.Write("<script>alert('Hospital creado con éxito.');</script>");
                    return RedirectToAction("Hospitals");
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Mensaje))
                        Response.Write("<script>alert('No se pudo crear el hospital, intente nuevamente.');</script>");
                    else
                        Response.Write(string.Format("<script>alert('Error al crear el hospital: {0}');</script>", result.Mensaje));
                }


            }
            return View(objHosp);
        }

        public ActionResult Doctors()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            DoctorsModel model = new DoctorsModel();
            return View(model.obtenerDoctores());
        }

        public ActionResult CreateDoctor()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            Medicos m = new Medicos();
            HospitalesModel model = new HospitalesModel();
            var hospitales = model.obtenerHospitalesSelect();

            m.Hospitals = hospitales;
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(Medicos objMed)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            if (ModelState.IsValid)
            {
                DoctorsModel model = new DoctorsModel();
                var result = model.registrarMedico(objMed);

                if (result.Resultado)
                {
                    Response.Write("<script>alert('Médico registrado con éxito.');</script>");
                    return RedirectToAction("Doctors");
                }
                else
                {
                    if (string.IsNullOrEmpty(result.Mensaje))
                        Response.Write("<script>alert('No se pudo registrar al médico, intente nuevamente.');</script>");
                    else
                        Response.Write(string.Format("<script>alert('Error al registrar al médico: {0}');</script>", result.Mensaje));
                }


            }
            return View(objMed);
        }

        public ActionResult AsignatePatients(int id)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            UsuariosModel model = new UsuariosModel();
            return View(model.obtenerPacientes(id));            
        }

        [HttpPost]
        public ActionResult AsignatePatients(FormCollection form)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            string selected = Request.Form["chkSeleccionado"].ToString();
            Resultados result = new Resultados();
            UsuariosModel model = new UsuariosModel();
            var idMedico = int.Parse(Request.Url.Segments[3]);

            result = model.asignarPaciente(selected, idMedico);

            if (result.Resultado)
            {
                Response.Write("<script>alert('Paciente asignado con éxito.');</script>");
                return RedirectToAction("Doctors");
            }
            else
            {
                if (string.IsNullOrEmpty(result.Mensaje))
                    Response.Write("<script>alert('No se pudo registrar el paciente al médico, intente nuevamente.');</script>");
                else
                    Response.Write(string.Format("<script>alert('Error al asignar el paciente al médico: {0}');</script>", result.Mensaje));
            }

            return RedirectToAction("AsignatePatients", new { id = idMedico });
        }

        public ActionResult Reports()
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            VisitasMedicasModel vModel = new VisitasMedicasModel();
            DoctorsModel dModel = new DoctorsModel();
            HospitalesModel hModel = new HospitalesModel();
            UsuariosModel uModel = new UsuariosModel();
            
            ViewBag.Medicos = dModel.obtenerDoctorsSelect();
            ViewBag.Clinicas = hModel.obtenerHospitalesSelect();
            ViewBag.Pacientes = uModel.obtenerPacientesSelect();
            return View(vModel.obtenerVisitasReporte(null, null, null));
        }

        [HttpPost]
        public ActionResult Reports(FormCollection form)
        {
            if (isNotLoged())
                return RedirectToAction("Login", "Home");

            int ? idUsuario = object.Equals(null, Request.Form["idUsuario"]) ? null : (string.IsNullOrEmpty(Request.Form["idUsuario"]) ? null : new int?(int.Parse(Request.Form["idUsuario"].ToString())));
            int? idHospital = object.Equals(null, Request.Form["idHospital"]) ? null : (string.IsNullOrEmpty(Request.Form["idHospital"]) ? null : new int?(int.Parse(Request.Form["idHospital"].ToString())));
            int? idMedico = object.Equals(null, Request.Form["idMedico"]) ? null : (string.IsNullOrEmpty(Request.Form["idMedico"]) ? null : new int?(int.Parse(Request.Form["idMedico"].ToString())));

            VisitasMedicasModel model = new VisitasMedicasModel();

            List<Visitas> visitas = model.obtenerVisitasReporte(idUsuario, idHospital, idMedico);

            if (visitas.Count == 0)
            {
                Response.Write("<script>alert('No se encontraron resultados con los filtros seleccionados.');</script>");
            }            

            DoctorsModel dModel = new DoctorsModel();
            HospitalesModel hModel = new HospitalesModel();
            UsuariosModel uModel = new UsuariosModel();

            ViewBag.Medicos = dModel.obtenerDoctorsSelect();
            ViewBag.Clinicas = hModel.obtenerHospitalesSelect();
            ViewBag.Pacientes = uModel.obtenerPacientesSelect();
            return View(visitas);            
        }

        private bool isNotLoged()
        {
            return object.Equals(null, Session["UserID"]);
        }
    }
}