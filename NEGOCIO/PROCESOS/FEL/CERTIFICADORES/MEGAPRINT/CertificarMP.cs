using BIPS.MODELOS;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using System.Net.Http.Json;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class CertificarMP
    {

        public static string MessageResult;
        public static string UuidCer;
        public static string XMLCer;
        ResponseOK RespuestaCertificada = new ResponseOK();
        public async Task<bool> RegistrarDocumentoMP(string XMLFirmado, ConfiguracionesFel oConfiFel, string UuidReferencia)
        {
            bool RequestResult = false;
            try
            {                
                string UuidRegistradoMP;
                string DocumentoCertificado;
                XmlDocument DocFirmar = new XmlDocument();

                try
                {
                    XmlNode NodFirmarSGML = DocFirmar.CreateElement("RegistraDocumentoXMLRequest");
                    DocFirmar.AppendChild(NodFirmarSGML);

                    XmlDeclaration Declaraciones;
                    Declaraciones = DocFirmar.CreateXmlDeclaration("1.0", null, null);
                    Declaraciones.Encoding = "UTF-8";
                    DocFirmar.InsertBefore(Declaraciones, NodFirmarSGML);

                    XmlAttribute IdContent = DocFirmar.CreateAttribute("id");
                    IdContent.Value = UuidReferencia;
                    NodFirmarSGML.Attributes.Append(IdContent);

                    XmlNode xml_DTE = DocFirmar.CreateElement("xml_dte");
                    NodFirmarSGML.AppendChild(xml_DTE);
                    xml_DTE.InnerXml = $"<![CDATA[{XMLFirmado}]]>";
                    // xml_DTE.InnerText = $"<![CDATA[{ XMLString}]]>" ;
                }
                catch (Exception e)
                {
                    MessageResult ="Erro: " + e.Message;
                }

                try
                {
                    using (HttpClient hCliente = new HttpClient())
                    {
                        using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.Urlcertificar.Trim()))
                        {                            
                            requestMessage.Headers.Add("Method", "POST");
                            requestMessage.Headers.Add("Accept", "Application/xml");
                            requestMessage.Headers.Add("Authorization", "Bearer " + oConfiFel.Token.Trim());
                            //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                            requestMessage.Content = new StringContent(DocFirmar.OuterXml.ToString(), Encoding.UTF8, "Application/xml");

                            var response = await hCliente.SendAsync(requestMessage);
                            var Contenido = response.Content.ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                try
                                {
                                    int TipoRespuesta;
                                    XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                                    var Query1 = from doc in XMLRespuesta.Elements("RegistraDocumentoXMLResponse").Elements("tipo_respuesta") select doc;
                                    TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);

                                    if (TipoRespuesta == 0)
                                    {
                                        var Queryuuid = from doc in XMLRespuesta.Elements("RegistraDocumentoXMLResponse").Elements("uuid") select doc;
                                        UuidCer = Convert.ToString(Queryuuid.FirstOrDefault().Value);

                                        var Queryxml = from doc in XMLRespuesta.Elements("RegistraDocumentoXMLResponse").Elements("xml_dte") select doc;
                                        XMLCer = Convert.ToString(Queryxml.FirstOrDefault().Value);

                                        string Serie = UuidCer.Substring(0, 8);
                                        string NumeroHexa = UuidCer.Substring(9, 9);
                                        NumeroHexa = NumeroHexa.Replace("-", "");

                                        Int64 NumeroAutorizacion = Convert.ToInt64(NumeroHexa, 16); // HexadecimalToDecimal(NumeroHexa);
                                        string Autorizacion = Convert.ToString(NumeroAutorizacion);

                                        var objCertificado = new ResponseOK()
                                        {

                                            uuid = UuidCer,
                                            serie = Serie,
                                            numero = Autorizacion                                            
                                        };

                                        RespuestaCertificada = objCertificado;

                                    }
                                    else
                                    {                                        
                                        RequestResult = false;                                        
                                        var QueryError = from Docto in XMLRespuesta.Elements("listado_errores").Elements() select Docto;

                                        MessageResult = "Error al Solicitar el Cambio de Token : ";
                                        int contador = 1;
                                        foreach (var node in QueryError)
                                        {
                                            MessageResult = MessageResult + $" | Error {contador}= {node.Value}";
                                            contador++;
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    MessageResult = "Error: " + e.Message;
                                    RequestResult = false;
                                }
                            }
                            else
                            {
                                MessageResult = "Error: El servidor retorno  " + response.StatusCode;
                                RequestResult = false;
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    RequestResult = false;
                    MessageResult = "Error: " + e.Message;
                } 
            }
            catch (Exception e)
            {
                RequestResult = false;
                MessageResult = "Error: " + e.Message;
            }
            return RequestResult;
        }

        public string UuidCertificado() => UuidCer;
        public ResponseOK MiCertificacion() => RespuestaCertificada;
    }
}
