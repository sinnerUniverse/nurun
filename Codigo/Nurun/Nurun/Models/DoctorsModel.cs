using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class DoctorsModel
    {
        public List<Medicos> obtenerHospitales()
        {
            using (NurunEntities db = new NurunEntities())
            {
                var medicos = db.Medicos.ToList<Medicos>();
                return medicos;
            }
        }
    }
}