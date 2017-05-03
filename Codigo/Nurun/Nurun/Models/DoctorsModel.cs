using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class DoctorsModel
    {
        public List<Medicos> obtenerDoctores()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var medicos = db.Medicos.Join(db.Hospitales
                    , m => m.idHospital
                    , h => h.IdHospital
                    , (m, h) => new Medicos()
                    {
                        idMedico = m.idMedico,
                        idHospital = m.idHospital,
                        Nombres = m.Nombres,
                        Apellidos = m.Apellidos,
                        HospitalNombre = h.Nombre
                    }).ToList<Medicos>();
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
    }
}