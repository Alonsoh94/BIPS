using BIPS.MODELOS;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class AnularINFILE
    {
        public string? nit_emisor { get; set; }
        public string? correo_copia { get; set; }
        public string? xml_dte { get; set; }


        public static string MensajeRequest;
        public static bool ResultadoRequest;
        public static string NumeroDoctoAn;
        public static string NumeroAutorizacionAn;
        public static string SerieAn;
        public async Task<bool> AnularDocumento(AnularINFILE ObjAnular, ConfiguracionesFel oConfiFel, string Referencia, long IdFact)
        {
          
            try
            {
                var ObjCertificarJson = JsonConvert.SerializeObject(ObjAnular);

                // string URI = "https://certificador.feel.com.gt/fel/anulacion/v2/dte";

                using (HttpClient hCliente = new HttpClient())
                {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.Urlanular.Trim()))
                    {
                        // requestMessage.Headers.Add("Content-Type", "application/json");
                        requestMessage.Headers.Add("Accept", "application/json");
                        requestMessage.Headers.Add("Method", "POST");
                        requestMessage.Headers.Add("usuario", oConfiFel.Usuario.Trim());
                        requestMessage.Headers.Add("identificador", Referencia);
                        requestMessage.Headers.Add("llave", oConfiFel.KeyId.Trim());
                        requestMessage.Content = new StringContent(ObjCertificarJson, Encoding.UTF8, "application/json");

                        var response = await hCliente.SendAsync(requestMessage);
                        var Contenido = response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            var JsonContent = JsonConvert.DeserializeObject<ResponseOK>(Contenido.Result);


                            if (JsonContent.resultado == "true")
                            {
                                NumeroAutorizacionAn = JsonContent.uuid;
                                NumeroDoctoAn = JsonContent.numero;
                                SerieAn = JsonContent.serie;

                                ResultadoRequest = true;
                                /*
                                try
                                {
                                  // try
                                  // {
                                  //     using (BIPSContext dbContext = new BIPSContext())
                                  //     {
                                  //         var ObjFact = new Factura()
                                  //         {
                                  //             Id = IdFact,
                                  //             Anulado = true,
                                  //             NumeroAutorizacionA = JsonContent.uuid,
                                  //             NumeroDoctoA = JsonContent.numero,
                                  //             SerieA = JsonContent.serie,
                                  //             FechaAnulado = DateTime.Now,
                                  //             EstadoGeneral = false   
                                  //         };
                                  //         dbContext.Entry(ObjFact).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                                  //         //dbContext.Facturas.Update(ObjFact);
                                  //         dbContext.SaveChanges();
                                  //         ResultadoRequest = true;
                                  //
                                  //     }
                                  //
                                  // }
                                  // catch (Exception e)
                                  // {
                                  //     MensajeRequest = "Error al Intentar actualziar los datos de Factura: " + e.Message;
                                  //     ResultadoRequest = false;
                                  // }
                                    
                                    

                                }
                                catch (Exception e)
                                {
                                    MensajeRequest = "Error al Intentar Anular el Documento: " + e.Message;
                                    ResultadoRequest = false;
                                }
                                */
                            }
                            else
                            {
                                string ListaErrores = string.Empty;
                                foreach (var error in JsonContent.descripcion_errores)
                                {
                                    ListaErrores += error.numeral;
                                    ListaErrores += " : ";
                                    ListaErrores += error.mensaje_error;
                                    ListaErrores += " **** ";
                                }
                                MensajeRequest = ListaErrores;
                            }

                        }
                        else
                        {
                            ResultadoRequest = false;
                            MensajeRequest = "Error del Servidor del Certificador, Código de estado: " + response.StatusCode;
                        }

                    }
                }

            }
            catch (Exception e)
            {
                MensajeRequest = "Error :" + e.Message;
            }
            return ResultadoRequest;
            
        }
        public string MensajeResultado() => MensajeRequest;
        public string NumeroAutorizacionAnulado() => NumeroAutorizacionAn;
        public string NumeroDoctoAnulado()=> NumeroDoctoAn;
        public string SerieAnulado() => SerieAn;
    }
}
