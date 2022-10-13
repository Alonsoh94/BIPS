using BIPS.MODELOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class FirmarMP
    {
        int TipoRespuesta;
        static string UuidFirmado = string.Empty;
        static string XMLFirmado = string.Empty;
        string? Token;
        static string MensajeRequest = string.Empty;

        public async Task <bool> FirmarDocumento(String XMLString, string UuiReferencia, ConfiguracionesFel oConfiFel)
        {
            MensajeRequest = string.Empty;
            bool ResultadoRequest = false;

            XmlDocument DocFirmar = new XmlDocument();

            XmlNode NodFirmarSGML = DocFirmar.CreateElement("FirmaDocumentoRequest");
            DocFirmar.AppendChild(NodFirmarSGML);

            XmlDeclaration Declaraciones;
            Declaraciones = DocFirmar.CreateXmlDeclaration("1.0", null, null);
            Declaraciones.Encoding = "UTF-8";
            //Declaraciones.Standalone = "yes";
            DocFirmar.InsertBefore(Declaraciones, NodFirmarSGML);

            XmlAttribute IdContent = DocFirmar.CreateAttribute("id");
            IdContent.Value = UuiReferencia;
            NodFirmarSGML.Attributes.Append(IdContent);

            XmlNode xml_DTE = DocFirmar.CreateElement("xml_dte");
            NodFirmarSGML.AppendChild(xml_DTE);
            xml_DTE.InnerXml = $"<![CDATA[{XMLString}]]>";           

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.Urlfirmar.Trim()))
                {                  
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("Accept","Application/xml");
                    requestMessage.Headers.Add("Authorization", "Bearer " + oConfiFel.Token.Trim());
                    //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                    requestMessage.Content = new StringContent(DocFirmar.OuterXml.ToString(), Encoding.UTF8, "Application/xml");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {                       
                        try
                        {
                            XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                            var Query1 = from doc in XMLRespuesta.Elements("FirmaDocumentoResponse").Elements("tipo_respuesta") select doc;
                            TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);

                            if (TipoRespuesta == 0)
                            { 
                                var Queryuuid = from doc in XMLRespuesta.Elements("FirmaDocumentoResponse").Elements("uuid") select doc;
                                UuidFirmado = Convert.ToString(Queryuuid.FirstOrDefault().Value);

                                var Queryxml = from doc in XMLRespuesta.Elements("FirmaDocumentoResponse").Elements("xml_dte") select doc;
                                XMLFirmado = Convert.ToString(Queryxml.FirstOrDefault().Value);

                                ResultadoRequest = true;                                
                            }
                            else
                            {
                                ResultadoRequest = false;
                                string Errores;
                                var QueryError = from Docto in XMLRespuesta.Elements("listado_errores").Elements() select Docto;

                                Errores = "Error al Solicitar el Cambio de Token : ";
                                int contador = 1;
                                foreach (var node in QueryError)
                                {
                                    Errores = Errores + $" | Error {contador}= {node.Value}";
                                    contador++;
                                }
                                MensajeRequest = Errores;
                            }   
                        }
                        catch (Exception e)
                        {
                            ResultadoRequest = false;
                            MensajeRequest = "Error: " + e.Message;
                        }
                    }
                    else
                    {
                        ResultadoRequest = false;
                        MensajeRequest = "Error: Status Code = " + response.StatusCode;
                    }
                }
            }

            return ResultadoRequest;
        }

        public string UuidXMLFimardo() => UuidFirmado;
        public string XMLFirmandoDoc() => XMLFirmado;
        public string MensajeResultado() => MensajeRequest;
    }
}
