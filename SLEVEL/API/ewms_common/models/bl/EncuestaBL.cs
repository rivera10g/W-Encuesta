using ewms.common.models.entity;
using ewms.common.models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.bl
{
    public class EncuestaBL
    {
        private Entities dbLocal = new Entities();

        //create
        public bcgResult CrearEncuesta(EncuestaViewModel encuesta)
        {
            bcgResult result = new bcgResult();

            try
            {
                TBL_ENCUESTAS addNew = new TBL_ENCUESTAS();
                addNew.NOMBRE = encuesta.nombre;
                addNew.DESCRIPCION = encuesta.descripcion;
                addNew.LINKUNICO = GenerarEnlaceUnico(); 

                dbLocal.TBL_ENCUESTAS.Add(addNew);
                dbLocal.SaveChanges();
                result.success = true;
                result.message = "Ingresar al siguiente enlace para llenar la encuesta: " + addNew.LINKUNICO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error al crear la encuesta", ex);
            }

            return result;
        }

        //update
        public bcgResult EditarEncuesta(int id, string newNombre, string newDescripcion)
        {
            bcgResult result = new bcgResult();
            var Edit = new TBL_ENCUESTAS();

            try
            {
                Edit = dbLocal.TBL_ENCUESTAS.Where(x => x.ID == id).FirstOrDefault();

                if (Edit != null)
                {
                    Edit.NOMBRE = newNombre;
                    Edit.DESCRIPCION = newDescripcion;
                    Edit.LINKUNICO = GenerarEnlaceUnico();

                    dbLocal.SaveChanges();
                    result.success = true;
                    result.message = "Encuesta editada correctamente, ingresar a este nuevo enlace para llenar la encuesta: " + Edit.LINKUNICO;
                }
                else
                {
                    result.success = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error", ex);
            }

            return result;
        }

        //delete
        public bcgResult EliminarEncuesta(int id)
        {
            bcgResult iResult = new bcgResult();

            try
            {
                var query = (from p in dbLocal.TBL_ENCUESTAS
                             where p.ID == id
                             select p).FirstOrDefault();

                if (query == null)
                {
                    iResult.message = "Registro no econtrado";
                    iResult.success = false;
                    return iResult;
                }
                else
                {
                    var keyDuplicate = dbLocal.TBL_CAMPOS_ENCUESTA.FirstOrDefault(ag => ag.ENCUESTAID == id);
                    if (keyDuplicate != null)
                    {
                        iResult.message = "No se puede eliminar el registro debido a la relación existente en TBL_ALUMNO_GRADO";
                        iResult.success = false;
                        return iResult;
                    }

                    dbLocal.TBL_ENCUESTAS.Remove(query);
                    dbLocal.SaveChanges();
                    iResult.success = true;
                }

            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                iResult.success = false;
                iResult.message = "Error al eliminar registro";
            }
            return iResult;
        }

        public IEnumerable<EncuestaViewModel> VerEncuestas()
        {
            try
            {
                var query = from p in dbLocal.TBL_ENCUESTAS
                            join f in dbLocal.TBL_CAMPOS_ENCUESTA
                            on p.ID equals f.ENCUESTAID into camposEncuesta
                            select new EncuestaViewModel()
                            {
                                nombre = p.NOMBRE,
                                descripcion = p.DESCRIPCION,
                                linkUnico = p.LINKUNICO,
                                campos = camposEncuesta.Select(c => new CampoEncuestaViewModel
                                {
                                    encuestaId = c.ENCUESTAID,
                                    nombre = c.NOMBRE,
                                    titulo = c.TITULO,
                                    esRequerido = c.ESREQUERIDO,
                                    tipoCampo = c.TIPOCAMPO
                                }).ToList()
                            };

                return query.ToList().Distinct();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error", ex);
            }
        }


        ///////////////// campos-encuestas /////////////////

        public CampoEncuestaViewModel ObtenerCampos(string enlace)
        {
            var encuesta = dbLocal.TBL_ENCUESTAS.FirstOrDefault(e => e.LINKUNICO == enlace);

            CampoEncuestaViewModel json = null;

            if (encuesta != null)
            {
                int encuestaId = encuesta.ID;

                json = new CampoEncuestaViewModel
                {
                    encuestaId = encuestaId,
                    nombre = "",
                    titulo = "",
                    esRequerido = false,
                    tipoCampo = ""
                };
            }

            return json;
        }

        public bcgResult LlenaEncuesta(CampoEncuestaViewModel encuesta)
        {
            bcgResult result = new bcgResult();

            try
            {
                TBL_CAMPOS_ENCUESTA addNew = new TBL_CAMPOS_ENCUESTA();
                addNew.ENCUESTAID = encuesta.encuestaId;
                addNew.NOMBRE = encuesta.nombre;
                addNew.TITULO = encuesta.titulo;
                addNew.ESREQUERIDO = encuesta.esRequerido;
                addNew.TIPOCAMPO = encuesta.tipoCampo;

                dbLocal.TBL_CAMPOS_ENCUESTA.Add(addNew);
                dbLocal.SaveChanges();
                result.success = true;
                result.message = "Encuesta llenada correctamente";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ApplicationException("Error al llenar la encuesta", ex);
            }

            return result;
        }

        //GENERACION DE ENLACE
        public string GenerarEnlaceUnico()
        {
            string baseUrl = "https://encuesta";
            string uniqueId = GenerateRandomId(5) + ".com";
            string uniqueLink = $"{baseUrl}/{uniqueId}";
            return uniqueLink;
        }

        public string GenerateRandomId(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var id = new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return id;
        }
    }
}