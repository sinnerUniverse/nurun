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
    
    public partial class Hospitales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hospitales()
        {
            this.Medicos = new HashSet<Medicos>();
            this.RelHospitalMedico = new HashSet<RelHospitalMedico>();
        }
    
        public int IdHospital { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medicos> Medicos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelHospitalMedico> RelHospitalMedico { get; set; }
    }
}
