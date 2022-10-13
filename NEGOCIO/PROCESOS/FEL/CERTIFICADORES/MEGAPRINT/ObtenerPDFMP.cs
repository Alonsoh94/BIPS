using BIPS.MODELOS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class ObtenerPDFMP
    {
        bool RequestResult;
        static string MessageResult;
        public async Task<bool> ImpresionFactura(ConfiguracionesFel oConfiFel, string UuidRequest)
        {
            bool ResultRequest = false;

            int TipoRespuesta;
            XmlDocument XMLPDF = new XmlDocument();

            XmlNode NodoPDF = XMLPDF.CreateElement("RetornaPDFRequest");
            XMLPDF.AppendChild(NodoPDF);

            XmlNode uuid = XMLPDF.CreateElement("uuid");
            NodoPDF.AppendChild(uuid);
            uuid.InnerText = UuidRequest;

            try
            {
                using (HttpClient hCliente = new HttpClient())
                {
                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.UrlretornarPdf.Trim()))
                    {
                        requestMessage.Headers.Add("Method", "POST");
                        requestMessage.Headers.Add("Accept", "Application/xml");
                        requestMessage.Headers.Add("Authorization", "Bearer " + oConfiFel.Token.Trim());
                        //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                        requestMessage.Content = new StringContent(XMLPDF.OuterXml.ToString(), Encoding.UTF8, "Application/xml");

                        var response = await hCliente.SendAsync(requestMessage);
                        var Contenido = response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {                               
                                XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                                var Query1 = from doc in XMLRespuesta.Elements("RetornaPDFResponse").Elements("tipo_respuesta") select doc;
                                TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);

                                if (TipoRespuesta == 0)
                                {
                                    ResultRequest = true;
                                    var Query2 = from doc in XMLRespuesta.Elements("RetornaPDFResponse").Elements("pdf") select doc;
                                    string MiPDF = Convert.ToString(Query2.FirstOrDefault().Value);

                                    string PDFString = MiPDF;
                                    string ruta = oConfiFel.PathPdfgenerado + $@"\{UuidRequest}.pdf";
                                    byte[] PDFRecibido = Convert.FromBase64String(PDFString);
                                    System.IO.File.WriteAllBytes(ruta, PDFRecibido);

                                    using Process fileopener = new Process();
                                    fileopener.StartInfo.FileName = "explorer";
                                    fileopener.StartInfo.Arguments = oConfiFel.PathPdfgenerado + $@"\{UuidRequest}.pdf"; fileopener.Start();
                                   // Environment.Exit(0);

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
            return ResultRequest;
        }
    }
}
