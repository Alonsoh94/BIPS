using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class FirmarINFILE
    {
        public string llave { get; set; }
        public string codigo { get; set; }
        public string archivo { get; set; }
        public string alias { get; set; }
        public string es_anulacion { get; set; }

        public async Task FirmarDocumento(FirmarINFILE ObjFirmar)
        {
            var ObjFirmaJson = JsonConvert.SerializeObject(ObjFirmar);
            string URI = "https://signer-emisores.feel.com.gt/sign_solicitud_firmas/firma_xml";
            HttpClient Cliente;
            using(Cliente = new HttpClient())
            {
                
                //Cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //var Request = new HttpRequestMessage(HttpMethod.Post, URI);
               // Cliente.DefaultRequestHeaders.Add("Content-Type", "application/json");
               // Cliente.DefaultRequestHeaders.Add("Accept", "application/json");
               // Cliente.DefaultRequestHeaders.Add("Method", "POST");

                HttpContent httpContent = new StringContent(ObjFirmaJson);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                Cliente.DefaultRequestHeaders.Add("Accept", "application/json");
                Cliente.DefaultRequestHeaders.Add("Method", "POST");
                Cliente.DefaultRequestHeaders.Add("ContentType","application /json");

                var Respuesta = await  Cliente.PostAsJsonAsync(URI, httpContent);
                if (Respuesta.IsSuccessStatusCode)
                {
                    var Contenido = await Respuesta.Content.ReadAsStringAsync();
                  
                    string result = Contenido;
                }
                
            }
        }
    }
}
