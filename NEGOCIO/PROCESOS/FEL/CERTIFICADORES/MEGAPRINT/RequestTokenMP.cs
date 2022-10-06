using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT
{
    public class RequestTokenMP
    {
        public async Task<bool> RequestToken(ConfiguracionesFel oConfiFel)
        {
            bool ResultadoRequest = false;
            DateTime Vigencia;
            BIPSContext dbContext;



            XmlDocument XmlToken = new XmlDocument();

            XmlNode NodoToken = XmlToken.CreateElement("SolicitaTokenRequest");
            XmlToken.AppendChild(NodoToken);


            XmlNode NodoUsuario = XmlToken.CreateElement("usuario");
            NodoToken.AppendChild(NodoUsuario);
            NodoUsuario.InnerText = oConfiFel.Usuario.Trim();

            XmlNode NodoApikey = XmlToken.CreateElement("apikey");
            NodoToken.AppendChild(NodoApikey);
            NodoApikey.InnerText = oConfiFel.Calve.Trim();

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.Urltoken.Trim()))
                {
                    // requestMessage.Headers.Add("Content-Type", "application/json");
                    requestMessage.Headers.Add("Accept", "Application/xml");
                    requestMessage.Headers.Add("Method", "POST");
                   // requestMessage.Headers.Add("usuario", "109641035");
                    //requestMessage.Headers.Add("identificador", "FACT-478533");
                   // requestMessage.Headers.Add("llave", "531C11258CF567F5985AB5F8C910B0D6");
                    requestMessage.Content = new StringContent(XmlToken.OuterXml, Encoding.UTF8, "Application/xml");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        int TipoRespuesta;
                        string Token;
                        try
                        {
                            XDocument XMLRespuesta = XDocument.Parse(Contenido.Result);
                            var Query1 = from doc in XMLRespuesta.Elements("SolicitaTokenResponse").Elements("tipo_respuesta") select doc;
                            TipoRespuesta = Convert.ToInt32(Query1.FirstOrDefault().Value);
                            
                            if(TipoRespuesta == 0)
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

                            
                        }
                        catch (Exception)
                        {

                            ResultadoRequest=false;
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
