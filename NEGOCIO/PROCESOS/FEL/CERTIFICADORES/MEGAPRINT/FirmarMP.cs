using BIPS.MODELOS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class FirmarMP
    {

        public async Task <bool> FirmarDocumento(string XMLString, string UuiReferencia, ConfiguracionesFel oConfiFel)
        {
            bool ResultadoRequest = false;
            XmlDocument DocFirmar = new XmlDocument();


            XmlNode NodFirmarSGML = DocFirmar.CreateElement("FirmaDocumentoRequest");
            DocFirmar.AppendChild(NodFirmarSGML);

            XmlDeclaration Declaraciones;
            Declaraciones = DocFirmar.CreateXmlDeclaration("1.0", null, null);
            Declaraciones.Encoding = "UTF-8";
            Declaraciones.Standalone = "yes";
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
                
                    //requestMessage.Headers.Add("Content-Type", "Application/xml");
                    requestMessage.Headers.Add("Accept", "Application/xml");
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("Accept","Application/xml");
                    requestMessage.Headers.Add("Authorization", "Bearer " + oConfiFel.Token);
                    //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                    requestMessage.Content = new StringContent( DocFirmar.OuterXml, Encoding.UTF8, "Application/xml");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        int TipoRespuesta;
                        string Token;
                        try
                        {
                            XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                            var Query1 = from doc in XMLRespuesta.Elements("FirmaDocumentoResponse").Elements("tipo_respuesta") select doc;
                            TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);

                           /* if (TipoRespuesta == 0)
                            {
                                var Query2 = from doc in XMLRespuesta.Elements("SolicitaTokenResponse").Elements("token") select doc;
                                var Query3 = from doc in XMLRespuesta.Elements("SolicitaTokenResponse").Elements("vigencia") select doc;
                                Token = Query2.FirstOrDefault().Value;
                                Vigencia = Convert.ToDateTime(Query3.FirstOrDefault().Value);

                                using (dbContext = new())
                                {
                                    var confi = dbContext.ConfiguracionesFels.First();
                                    confi.ExpiraToken = Vigencia;
                                    confi.Token = Token;
                                    dbContext.SaveChanges();
                                    ResultadoRequest = true;
                                }
                            }
                            else
                            {
                                ResultadoRequest = false;
                            }
                           */


                        }
                        catch (Exception)
                        {

                           // ResultadoRequest = false;
                        }

                    }
                    else
                    {
                        // ResultadoReq = false;
                    }

                }
            }

            return ResultadoRequest;
        }
    }
}
