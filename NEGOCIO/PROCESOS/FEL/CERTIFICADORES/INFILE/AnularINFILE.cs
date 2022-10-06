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


        public async Task AnularDocumento(AnularINFILE ObjAnular)
        {
            var ObjCertificarJson = JsonConvert.SerializeObject(ObjAnular);

            string URI = "https://certificador.feel.com.gt/fel/anulacion/v2/dte";

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, URI))
                {
                    // requestMessage.Headers.Add("Content-Type", "application/json");
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("usuario", "RAICES_DEMO");
                    requestMessage.Headers.Add("identificador", "FACT-478533");
                    requestMessage.Headers.Add("llave", "531C11258CF567F5985AB5F8C910B0D6");
                    requestMessage.Content = new StringContent(ObjCertificarJson, Encoding.UTF8, "application/json");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = JsonConvert.DeserializeObject<ResponseOK>(Contenido.Result);


                        if (JsonContent.resultado == "true")
                        {
                            // ArchivoReq = JsonContent.archivo;
                            // ResultadoReq = true;
                            // DescripcionReq = JsonContent.descripcion;
                            Console.WriteLine("");
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

                           // ArchivoReq = JsonContent.archivo;
                           // ResultadoReq = false;
                           // DescripcionReq = JsonContent.descripcion;
                        }

                    }
                    else
                    {
                       // ResultadoReq = false;
                    }

                }
            }
        }
    }
}
