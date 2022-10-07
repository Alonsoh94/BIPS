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

        public async Task <bool> FirmarDocumento(XmlDocument DoctoToSign, string UuiReferencia, ConfiguracionesFel oConfiFel)
        {
            bool ResultadoRequest = false;
            XmlDocument DocFirmar = new XmlDocument();

            string Dataxml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<dte:GTDocumento xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:dte=\"http://www.sat.gob.gt/dte/fel/0.2.0\" Version=\"0.1\">\r\n  <dte:SAT ClaseDocumento=\"dte\">\r\n    <dte:DTE ID=\"DatosCertificados\">\r\n      <dte:DatosEmision ID=\"DatosEmision\">\r\n        <dte:DatosGenerales CodigoMoneda=\"GTQ\" FechaHoraEmision=\"2022-10-03T00:00:00.000-06:00\" Tipo=\"FACT\" />\r\n        <dte:Emisor AfiliacionIVA=\"GEN\" CodigoEstablecimiento=\"1\" CorreoEmisor=\"jose.alonso@sidgt.com\" NITEmisor=\"109641035\" NombreComercial=\"INVERSIONES MORALES GARCÍA, S.A.\" NombreEmisor=\"JULIO CESAR ANDRINO GARCIA\">\r\n          <dte:DireccionEmisor>\r\n            <dte:Direccion>0 AVENIDA 0-11 ZONA 1 LOCAL A</dte:Direccion>\r\n            <dte:CodigoPostal>20022</dte:CodigoPostal>\r\n            <dte:Municipio>EL PROGRESO</dte:Municipio>\r\n            <dte:Departamento>JUTIAPA</dte:Departamento>\r\n            <dte:Pais>GT</dte:Pais>\r\n          </dte:DireccionEmisor>\r\n        </dte:Emisor>\r\n        <dte:Receptor CorreoReceptor=\"jose.alonso@sidgt.com\" IDReceptor=\"29842174\" NombreReceptor=\"AMPARO NAVAS\">\r\n          <dte:DireccionReceptor>\r\n            <dte:Direccion>EL PROGRESO, JUTIAPA</dte:Direccion>\r\n            <dte:CodigoPostal>22002</dte:CodigoPostal>\r\n            <dte:Municipio>GUATEMALA</dte:Municipio>\r\n            <dte:Departamento>GUATEMALA</dte:Departamento>\r\n            <dte:Pais>GT</dte:Pais>\r\n          </dte:DireccionReceptor>\r\n        </dte:Receptor>\r\n        <dte:Frases>\r\n          <dte:Frase CodigoEscenario=\"1\" TipoFrase=\"1\" />\r\n          <dte:Frase CodigoEscenario=\"9\" TipoFrase=\"4\" />\r\n        </dte:Frases>\r\n        <dte:Items>\r\n          <dte:Item NumeroLinea=\"1\" BienOServicio=\"B\">\r\n            <dte:Cantidad>1.0000</dte:Cantidad>\r\n            <dte:UnidadMedida>UNI</dte:UnidadMedida>\r\n            <dte:Descripcion>*  MONTELUKAST 10MG FARMANDINA X 10 TAB DUO|INICIAL</dte:Descripcion>\r\n            <dte:PrecioUnitario>143.3200</dte:PrecioUnitario>\r\n            <dte:Precio>143.32</dte:Precio>\r\n            <dte:Descuento>0.00</dte:Descuento>\r\n            <dte:Impuestos>\r\n              <dte:Impuesto>\r\n                <dte:NombreCorto>IVA</dte:NombreCorto>\r\n                <dte:CodigoUnidadGravable>2</dte:CodigoUnidadGravable>\r\n                <dte:MontoGravable>143.32</dte:MontoGravable>\r\n                <dte:MontoImpuesto>0.00</dte:MontoImpuesto>\r\n              </dte:Impuesto>\r\n            </dte:Impuestos>\r\n            <dte:Total>143.32</dte:Total>\r\n          </dte:Item>\r\n          <dte:Item NumeroLinea=\"2\" BienOServicio=\"B\">\r\n            <dte:Cantidad>1.0000</dte:Cantidad>\r\n            <dte:UnidadMedida>UNI</dte:UnidadMedida>\r\n            <dte:Descripcion> TINTE KUUL 6 RUBIO OSCURO|INICIAL</dte:Descripcion>\r\n            <dte:PrecioUnitario>25.0000</dte:PrecioUnitario>\r\n            <dte:Precio>25.00</dte:Precio>\r\n            <dte:Descuento>0.00</dte:Descuento>\r\n            <dte:Impuestos>\r\n              <dte:Impuesto>\r\n                <dte:NombreCorto>IVA</dte:NombreCorto>\r\n                <dte:CodigoUnidadGravable>1</dte:CodigoUnidadGravable>\r\n                <dte:MontoGravable>22.32</dte:MontoGravable>\r\n                <dte:MontoImpuesto>2.68</dte:MontoImpuesto>\r\n              </dte:Impuesto>\r\n            </dte:Impuestos>\r\n            <dte:Total>25.00</dte:Total>\r\n          </dte:Item>\r\n        </dte:Items>\r\n        <dte:Totales>\r\n          <dte:TotalImpuestos>\r\n            <dte:TotalImpuesto NombreCorto=\"IVA\" TotalMontoImpuesto=\"2.68\" />\r\n          </dte:TotalImpuestos>\r\n          <dte:GranTotal>168.32</dte:GranTotal>\r\n        </dte:Totales>\r\n      </dte:DatosEmision>\r\n    </dte:DTE>\r\n    <dte:Adenda>\r\n      <no_interno>A-474211</no_interno>\r\n      <establecimiento>CENTRO FARMACIA</establecimiento>\r\n      <nota_pedido>P. 16414</nota_pedido>\r\n      <vendedor>8</vendedor>\r\n      <forma_pago>CO</forma_pago>\r\n      <dias_credito>0</dias_credito>\r\n      <observaciones>Valor Exento:   Q.143.32</observaciones>\r\n    </dte:Adenda>\r\n  </dte:SAT>\r\n</dte:GTDocumento>";


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
            xml_DTE.InnerXml = $"<![CDATA[{Dataxml}]]>";

           

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, oConfiFel.Urlfirmar.Trim()))
                {
                
                    //requestMessage.Headers.Add("Content-Type", "Application/xml");
                   // requestMessage.Headers.Add("Accept", "Application/xml");
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("Accept","Application/xml");
                    requestMessage.Headers.Add("Authorization", "Bearer " + oConfiFel.Token);
                    //requestMessage.Content =  new StringContent(DocFirmar.OuterXml);                    
                    requestMessage.Content = new StringContent(DocFirmar.InnerXml, Encoding.UTF8, "Application/xml");

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
