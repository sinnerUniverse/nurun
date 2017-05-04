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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateHospital(Hospitales objHosp)
        {
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
            DoctorsModel model = new DoctorsModel();
            return View(model.obtenerDoctores());
        }

        public ActionResult CreateDoctor()
        {
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
            UsuariosModel model = new UsuariosModel();
            return View(model.obtenerPacientes(id));            
        }

        [HttpPost]
        public ActionResult AsignatePatients(FormCollection form)
        {
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
    }
}