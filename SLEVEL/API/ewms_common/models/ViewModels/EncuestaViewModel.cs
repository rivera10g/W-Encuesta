using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ewms.common.models.ViewModels
{
    public class EncuestaViewModel
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string linkUnico { get; set; }
        public List<CampoEncuestaViewModel> campos { get; set; }
    }


    public class CampoEncuestaViewModel
    {
        public int encuestaId { get; set; }
        public string nombre { get; set; }
        public string titulo { get; set; }
        public bool esRequerido { get; set; }
        public string tipoCampo { get; set; }
    }
}
