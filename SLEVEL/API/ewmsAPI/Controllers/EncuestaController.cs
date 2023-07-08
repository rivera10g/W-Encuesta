using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ewms.common.models.bl;
using ewms.common.models.ViewModels;
using System.Web.Http.Description;

namespace ewmsAPI.Controllers
{
    //[Authorize]
    public class EncuestaController : ApiController
    {
        EncuestaBL blEnc = new EncuestaBL();

        //create
        [Authorize]
        [ActionName("CrearEncuesta")]
        [HttpPost]
        [Route("api/Encuesta/CrearEncuesta")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult CrearEncuesta([FromBody] EncuestaViewModel encuesta)
        {
            return blEnc.CrearEncuesta(encuesta);
        }


        //update
        [Authorize]
        [ActionName("EditarEncuesta")]
        [HttpGet]
        [Route("api/Encuesta/EditarEncuesta")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult EditarEncuesta(int id, string newNombre, string newDescripcion)
        {
            return blEnc.EditarEncuesta(id, newNombre, newDescripcion);
        }

        //delete
        [Authorize]
        [ActionName("EliminarEncuesta")]
        [HttpGet]
        [Route("api/Encuesta/EliminarEncuesta")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult EliminarEncuesta(int id)
        {
            return blEnc.EliminarEncuesta(id);
        }

        ///////////////// campos-encuestas /////////////////

        //get
        [ActionName("ObtenerCampos")]
        [HttpGet]
        [Route("api/Encuesta/ObtenerCampos")]
        public IHttpActionResult ObtenerCampos(string enlace)
        {
            var campos = blEnc.ObtenerCampos(enlace);
            return Ok(campos);
        }

        //create
        [ActionName("LlenaEncuesta")]
        [HttpPost]
        [Route("api/Encuesta/LlenaEncuesta")]
        [ResponseType(typeof(bcgResult))]
        public bcgResult LlenaEncuesta([FromBody] CampoEncuestaViewModel encuesta)
        {
            return blEnc.LlenaEncuesta(encuesta);
        }

        //get
        [Authorize]
        [ActionName("VerEncuestas")]
        [HttpGet]
        [Route("api/Encuesta/VerEncuestas")]
        public IEnumerable<EncuestaViewModel> VerEncuestas()
        {
            return blEnc.VerEncuestas();
        }
    }
}