//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nurun.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisitaMedica
    {
        public int idVisita { get; set; }
        public System.DateTime FechaVisita { get; set; }
        public int idUsuario { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}