//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ewms.common.models.entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_ENCUESTAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBL_ENCUESTAS()
        {
            this.TBL_CAMPOS_ENCUESTA = new HashSet<TBL_CAMPOS_ENCUESTA>();
        }
    
        public int ID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string LINKUNICO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBL_CAMPOS_ENCUESTA> TBL_CAMPOS_ENCUESTA { get; set; }
    }
}
