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
    
    public partial class TBL_CAMPOS_ENCUESTA
    {
        public int ID { get; set; }
        public int ENCUESTAID { get; set; }
        public string NOMBRE { get; set; }
        public string TITULO { get; set; }
        public bool ESREQUERIDO { get; set; }
        public string TIPOCAMPO { get; set; }
    
        public virtual TBL_ENCUESTAS TBL_ENCUESTAS { get; set; }
    }
}