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

        public List<Visitas> obtenerVisitasReporte(int? idUsuario, int? idHospital, int? idMedico)
        {
            using (NurunEntities db = new NurunEntities())
            {

                var visitas = (from v in db.VisitaMedica
                               join u in db.Usuarios on v.idUsuario equals u.IdUsuario
                               join m in db.Medicos on u.IdMedico equals m.idMedico
                               join h in db.Hospitales on m.idHospital equals h.IdHospital
                               where (idUsuario == null || u.IdUsuario == idUsuario.Value)
                                && (idHospital == null || m.idHospital == idHospital.Value)
                                && (idMedico == null || u.IdMedico == idMedico.Value)
                               select new Visitas()
                               {
                                   Cita = v,
                                   MedicoAsignado = new MedicosDTO()
                                   {
                                       idMedico = m.idMedico,
                                       Nombres = m.Nombres,
                                       Apellidos = m.Apellidos,
                                       idHospital = m.idHospital,
                                       HospitalNombre = h.Nombre
                                   }
                               }).ToList<Visitas>();

                return visitas;

                // La forma fea 
                //if (object.Equals(null, idUsuario) && object.Equals(null, idHospital) && object.Equals(null, idMedico))
                //    return visitas;
                //else if (!object.Equals(null, idUsuario) && object.Equals(null, idHospital) && object.Equals(null, idMedico))
                //    return visitas.Where(v => v.Cita.idUsuario == idUsuario.Value).ToList<Visitas>();
                //else if (!object.Equals(null, idUsuario) && !object.Equals(null, idHospital) && object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.Cita.idUsuario == idUsuario.Value
                //        && v.MedicoAsignado.idHospital == idHospital.Value).ToList<Visitas>();
                //}
                //else if (!object.Equals(null, idUsuario) && !object.Equals(null, idHospital) && !object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.Cita.idUsuario == idUsuario.Value
                //        && v.MedicoAsignado.idHospital == idHospital.Value
                //        && v.MedicoAsignado.idMedico == idMedico.Value).ToList<Visitas>();
                //}
                //else if (!object.Equals(null, idUsuario) && object.Equals(null, idHospital) && !object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.Cita.idUsuario == idUsuario.Value
                //        && v.MedicoAsignado.idMedico == idMedico.Value).ToList<Visitas>();
                //}
                //else if (object.Equals(null, idUsuario) && !object.Equals(null, idHospital) && object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.MedicoAsignado.idHospital == idHospital.Value).ToList<Visitas>();
                //}
                //else if (object.Equals(null, idUsuario) && !object.Equals(null, idHospital) && !object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.MedicoAsignado.idHospital == idHospital.Value
                //        && v.MedicoAsignado.idMedico == idMedico.Value).ToList<Visitas>();
                //}
                //else if (object.Equals(null, idUsuario) && object.Equals(null, idHospital) && !object.Equals(null, idMedico))
                //{
                //    return visitas.Where(v =>
                //        v.MedicoAsignado.idMedico == idMedico.Value).ToList<Visitas>();
                //}
                //else
                //    return new List<Visitas>();

            }
        }
    }
}