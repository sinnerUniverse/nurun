using Nurun.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class Visitas
    {
        public VisitaMedica Cita { get; set; }
        public Medicos MedicoAsignado { get; set; }
    }
}