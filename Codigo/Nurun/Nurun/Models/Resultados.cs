using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nurun.Models
{
    public class Resultados
    {
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }

        public override string ToString()
        {
            return Mensaje;
        }
    }
}