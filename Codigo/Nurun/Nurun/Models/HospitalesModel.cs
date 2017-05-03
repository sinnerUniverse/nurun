using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}