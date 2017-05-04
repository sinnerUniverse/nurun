using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class VisitasMedicasModel
    {

        public Resultados registrarVisita(VisitaMedica objVis, int idUsuario)
        {
            using (NurunEntities db = new NurunEntities())
            {
                Resultados r = new Resultados();
                try
                {
                    objVis.idUsuario = idUsuario;
                    db.VisitaMedica.Add(objVis);
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

        public List<Visitas> obtenerVisitas(int idUsuario)
        {
            using (NurunEntities db = new NurunEntities())
            {

                var visitas = (from v in db.VisitaMedica
                               join u in db.Usuarios on v.idUsuario equals u.IdUsuario
                               join m in db.Medicos on u.IdMedico equals m.idMedico
                               join h in db.Hospitales on m.idHospital equals h.IdHospital
                               where v.idUsuario == idUsuario
                               select new Visitas()
                               {
                                  Cita = v,
                                  MedicoAsignado = new MedicosDTO()
                                  {
                                      idMedico = m.idMedico,
                                      Nombres = m.Nombres,
                                      Apellidos = m.Apellidos,
                                      HospitalNombre = h.Nombre
                                  }
                               }).ToList<Visitas>();

                return visitas;
            }
        }
    }
}