using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nurun.Models
{
    public class HospitalesModel
    {
        public List<Hospitales> obtenerHospitales()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var hospitales = db.Hospitales.ToList<Hospitales>();
                return hospitales;
            }
        }

        public IEnumerable<SelectListItem> obtenerHospitalesSelect()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var hospitales = db.Hospitales.ToList<Hospitales>().Select(x =>
                        new SelectListItem
                        {
                            Value = x.IdHospital.ToString(),
                            Text = x.Nombre
                        });

                return new SelectList(hospitales, "Value", "Text");
            }
        }

        public Resultados crearHospital(Hospitales hospital)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                try
                {
                    hospital.FechaCreacion = DateTime.Now;
                    db.Hospitales.Add(hospital);
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
    }
}