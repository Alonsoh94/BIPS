using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            // Metodo Funcional con RestSharp
            var ObjFirmaJson = JsonConvert.SerializeObject(ObjFirmar);
            string URI = "https://signer-emisores.feel.com.gt/sign_solicitud_firmas/firma_xml";

            var MyCliente = new RestClient(URI);
          
            var MyRequest = new RestRequest();  
            MyRequest.AddHeader("Content-Type", "application/json");
            MyRequest.AddHeader("Accept", "application/json");
            //MyRequest.AddOrUpdateHeader("Method", "POST");
            MyRequest.Method = Method.Post;
            MyRequest.RequestFormat = DataFormat.Json;
            //MyRequest.RequestFormat = DataFormat.Xml;
            MyRequest.AddBody(ObjFirmaJson);          
            
            RestResponse Response = await MyCliente.PostAsync(MyRequest);            
            if (Response.IsSuccessStatusCode)
            {
                string contenido = Response.Content;
            }

            // +++
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URI);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            request.Content = new StringContent(ObjFirmaJson, Encoding.UTF8, "application/json");
            var Responses = client.SendAsync(request);
            if (Responses.IsCompleted)
            {
                var datos = Responses.Result;
            }




            // Metodo Funcional

            var requestData = (HttpWebRequest)WebRequest.Create(URI);
            requestData.Method = "POST";
            requestData.ContentType = "application/json";
            requestData.Accept = "application/json";

            using (var streamWriter = new StreamWriter(requestData.GetRequestStream()))
            {
                streamWriter.Write(ObjFirmaJson);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                using (WebResponse response = requestData.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }

            // ++++++++





            HttpClient Cliente;
            using(Cliente = new HttpClient())
            {

                //Cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //var Request = new HttpRequestMessage(HttpMethod.Post, URI);
                // Cliente.DefaultRequestHeaders.Add("Content-Type", "application/json");
                // Cliente.DefaultRequestHeaders.Add("Accept", "application/json");
                // Cliente.DefaultRequestHeaders.Add("Method", "POST");
                //Cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var Request = new HttpRequestMessage(HttpMethod.Post, URI);
                Request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
               // Request.Headers.Add("Content-Type", "application/json");
                Request.Headers.Add("Accept", "application/json");
                Request.Headers.Add("Method", "POST");


              // HttpContent httpContent = new StringContent(ObjFirmaJson);
              // httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
              // Cliente.DefaultRequestHeaders.Add("Accept", "application/json");
              // Cliente.DefaultRequestHeaders.Add("Method", "POST");
              // Cliente.DefaultRequestHeaders.Add("ContentType","application /json");

                var Respuesta = await  Cliente.PostAsJsonAsync(URI, Request);
                if (Respuesta.IsSuccessStatusCode)
                {
                    var Contenido = await Respuesta.Content.ReadAsStringAsync();
                  
                    string result = Contenido;
                }
                
            }
        }
    }
}
