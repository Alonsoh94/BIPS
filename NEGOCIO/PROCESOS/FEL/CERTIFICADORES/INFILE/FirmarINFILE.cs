using BIPS.MODELOS;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class FirmarINFILE
    {
        public string llave { get; set; }
        public string codigo { get; set; }
        public string archivo { get; set; }
        public string alias { get; set; }
        public string es_anulacion { get; set; }



        public static string ArchivoReq;
        public static bool ResultadoReq;
        public static string DescripcionReq;
        public async Task<bool> FirmarDocumento(FirmarINFILE ObjFirmar, ConfiguracionesFel confi)
        {
            // Metodo Funcional con RestSharp
            var ObjFirmaJson = JsonConvert.SerializeObject(ObjFirmar);
           // string URI = "https://signer-emisores.feel.com.gt/sign_solicitud_firmas/firma_xml";


            //********************************

            using(HttpClient hCliente = new HttpClient())
            {
                using(var requestMessage = new HttpRequestMessage(HttpMethod.Post, confi.Urlfirmar.Trim()))
                {
                   // requestMessage.Headers.Add("Content-Type", "application/json");
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Content = new StringContent(ObjFirmaJson, Encoding.UTF8, "application/json");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();
                    
                    if(response.IsSuccessStatusCode)
                    {
                        var JsonContent = JsonConvert.DeserializeObject<ResponseOK>(Contenido.Result);
                        

                        if (JsonContent.resultado == "true")
                        {
                            ArchivoReq = JsonContent.archivo;
                            ResultadoReq = true;
                            DescripcionReq = JsonContent.descripcion;
                        }
                        else
                        {
                            ArchivoReq = JsonContent.archivo;
                            ResultadoReq = false;
                            DescripcionReq = JsonContent.descripcion;
                        }

                    }
                    else
                    {
                        ResultadoReq = false;
                    }
                   
                }
            }

            return ResultadoReq;
        }
        public string ArchivoXMLFirmado() => ArchivoReq;
        public string DescripcionXMLFirmado() => DescripcionReq;
        public bool ResultadoXMLFirmado()=> ResultadoReq;
    }

   
}
