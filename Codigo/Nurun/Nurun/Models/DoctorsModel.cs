using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Models
{
    public class DoctorsModel
    {
        public List<MedicosDTO> obtenerDoctores()
        {
            using (NurunEntities db = new NurunEntities())
            {

                var medicos = (from m in db.Medicos
                               join h in db.Hospitales on m.idHospital equals h.IdHospital
                               select new MedicosDTO()
                               {
                                   idMedico = m.idMedico,
                                   idHospital = m.idHospital,
                                   Nombres = m.Nombres,
                                   Apellidos = m.Apellidos,
                                   HospitalNombre = h.Nombre
                               }).ToList<MedicosDTO>();

                return medicos; 
            }
        }

        public Resultados registrarMedico(Medicos medico)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                try
                {
                    medico.FechaCreacion = DateTime.Now;
                    db.Medicos.Add(medico);
                    db.SaveChanges();
                    r.Resultado = true;
                }
                catch (Exception ex)
                {
                    r.Mensaje = ex.Message;
                    r.Resultado = false;
                }

                return r;
            }
        }

        public IEnumerable<SelectListItem> obtenerDoctorsSelect()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var doctors = db.Medicos.ToList<Medicos>().Select(x =>
                        new SelectListItem
                        {
                            Value = x.idMedico.ToString(),
                            Text = string.Format("{0} {1}", x.Nombres, x.Apellidos)
                        });

                return new SelectList(doctors, "Value", "Text");
            }
        }
    }
}