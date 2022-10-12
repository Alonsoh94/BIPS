using BIPS.MODELOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class VerificarDocumento
    {
        static string ResultadoVerificacion = string.Empty;
        public async Task<bool> VerificacionDocumento(ConfiguracionesFel ConfiFel, string UuidRef)
        {
            bool ResultadoVerificar = false;
            //int respuesta = 0;
            

            XmlDocument DocVerificar = new XmlDocument();


            XmlNode NodoVerificacion = DocVerificar.CreateElement("VerificaDocumentoRequest");
            DocVerificar.AppendChild(NodoVerificacion);

            XmlDeclaration Declaraciones;
            Declaraciones = DocVerificar.CreateXmlDeclaration("1.0", null, null);
            Declaraciones.Encoding = "UTF-8";
            DocVerificar.InsertBefore(Declaraciones, NodoVerificacion);

            XmlAttribute IdContent = DocVerificar.CreateAttribute("id");
            IdContent.Value = UuidRef;
            NodoVerificacion.Attributes.Append(IdContent);



            //****

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, ConfiFel.UrlverificarDocumento.Trim()))
                {
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("Accept", "Application/xml");
                    requestMessage.Headers.Add("Authorization", "Bearer " + ConfiFel.Token.Trim());
                    //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                    requestMessage.Content = new StringContent(DocVerificar.OuterXml.ToString(), Encoding.UTF8, "Application/xml");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {

                        try
                        {
                            int TipoRespuesta;

                            XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                            var Query1 = from doc in XMLRespuesta.Elements("VerificaDocumentoResponse").Elements("tipo_respuesta") select doc;
                            TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);

                            if (TipoRespuesta == 0)
                            {
                                ResultadoVerificar = true;
                            }
                            else
                            {
                                ResultadoVerificar = false;                                
                            }                          

                        }
                        catch (Exception)
                        {

                            ResultadoVerificar = false;
                        }

                    }
                    else
                    {
                         ResultadoVerificar = false;
                    }

                }
            }

            
            return ResultadoVerificar;

        }

        public string ResultadoVerificacionDocto() => ResultadoVerificacion;
    }
}
