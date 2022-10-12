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
        static string UuidFirmado;
        static string XMLFirmado;
        string Token;
        static string MensajeRequest = string.Empty;

        public async Task <bool> FirmarDocumento(String XMLString, string UuiReferencia, ConfiguracionesFel oConfiFel)
        {
            MensajeRequest = string.Empty;

            bool ResultadoRequest = false;
            XmlDocument DocFirmar = new XmlDocument();

            //string Dataxml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<dte:GTDocumento xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:dte=\"http://www.sat.gob.gt/dte/fel/0.2.0\" Version=\"0.1\">\r\n  <dte:SAT ClaseDocumento=\"dte\">\r\n    <dte:DTE ID=\"DatosCertificados\">\r\n      <dte:DatosEmision ID=\"DatosEmision\">\r\n        <dte:DatosGenerales CodigoMoneda=\"GTQ\" FechaHoraEmision=\"2022-10-03T00:00:00.000-06:00\" Tipo=\"FACT\" />\r\n        <dte:Emisor AfiliacionIVA=\"GEN\" CodigoEstablecimiento=\"1\" CorreoEmisor=\"jose.alonso@sidgt.com\" NITEmisor=\"109641035\" NombreComercial=\"INVERSIONES MORALES GARCÍA, S.A.\" NombreEmisor=\"JULIO CESAR ANDRINO GARCIA\">\r\n          <dte:DireccionEmisor>\r\n            <dte:Direccion>0 AVENIDA 0-11 ZONA 1 LOCAL A</dte:Direccion>\r\n            <dte:CodigoPostal>20022</dte:CodigoPostal>\r\n            <dte:Municipio>EL PROGRESO</dte:Municipio>\r\n            <dte:Departamento>JUTIAPA</dte:Departamento>\r\n            <dte:Pais>GT</dte:Pais>\r\n          </dte:DireccionEmisor>\r\n        </dte:Emisor>\r\n        <dte:Receptor CorreoReceptor=\"jose.alonso@sidgt.com\" IDReceptor=\"29842174\" NombreReceptor=\"AMPARO NAVAS\">\r\n          <dte:DireccionReceptor>\r\n            <dte:Direccion>EL PROGRESO, JUTIAPA</dte:Direccion>\r\n            <dte:CodigoPostal>22002</dte:CodigoPostal>\r\n            <dte:Municipio>GUATEMALA</dte:Municipio>\r\n            <dte:Departamento>GUATEMALA</dte:Departamento>\r\n            <dte:Pais>GT</dte:Pais>\r\n          </dte:DireccionReceptor>\r\n        </dte:Receptor>\r\n        <dte:Frases>\r\n          <dte:Frase CodigoEscenario=\"1\" TipoFrase=\"1\" />\r\n          <dte:Frase CodigoEscenario=\"9\" TipoFrase=\"4\" />\r\n        </dte:Frases>\r\n        <dte:Items>\r\n          <dte:Item NumeroLinea=\"1\" BienOServicio=\"B\">\r\n            <dte:Cantidad>1.0000</dte:Cantidad>\r\n            <dte:UnidadMedida>UNI</dte:UnidadMedida>\r\n            <dte:Descripcion>*  MONTELUKAST 10MG FARMANDINA X 10 TAB DUO|INICIAL</dte:Descripcion>\r\n            <dte:PrecioUnitario>143.3200</dte:PrecioUnitario>\r\n            <dte:Precio>143.32</dte:Precio>\r\n            <dte:Descuento>0.00</dte:Descuento>\r\n            <dte:Impuestos>\r\n              <dte:Impuesto>\r\n                <dte:NombreCorto>IVA</dte:NombreCorto>\r\n                <dte:CodigoUnidadGravable>2</dte:CodigoUnidadGravable>\r\n                <dte:MontoGravable>143.32</dte:MontoGravable>\r\n                <dte:MontoImpuesto>0.00</dte:MontoImpuesto>\r\n              </dte:Impuesto>\r\n            </dte:Impuestos>\r\n            <dte:Total>143.32</dte:Total>\r\n          </dte:Item>\r\n          <dte:Item NumeroLinea=\"2\" BienOServicio=\"B\">\r\n            <dte:Cantidad>1.0000</dte:Cantidad>\r\n            <dte:UnidadMedida>UNI</dte:UnidadMedida>\r\n            <dte:Descripcion> TINTE KUUL 6 RUBIO OSCURO|INICIAL</dte:Descripcion>\r\n            <dte:PrecioUnitario>25.0000</dte:PrecioUnitario>\r\n            <dte:Precio>25.00</dte:Precio>\r\n            <dte:Descuento>0.00</dte:Descuento>\r\n            <dte:Impuestos>\r\n              <dte:Impuesto>\r\n                <dte:NombreCorto>IVA</dte:NombreCorto>\r\n                <dte:CodigoUnidadGravable>1</dte:CodigoUnidadGravable>\r\n                <dte:MontoGravable>22.32</dte:MontoGravable>\r\n                <dte:MontoImpuesto>2.68</dte:MontoImpuesto>\r\n              </dte:Impuesto>\r\n            </dte:Impuestos>\r\n            <dte:Total>25.00</dte:Total>\r\n          </dte:Item>\r\n        </dte:Items>\r\n        <dte:Totales>\r\n          <dte:TotalImpuestos>\r\n            <dte:TotalImpuesto NombreCorto=\"IVA\" TotalMontoImpuesto=\"2.68\" />\r\n          </dte:TotalImpuestos>\r\n          <dte:GranTotal>168.32</dte:GranTotal>\r\n        </dte:Totales>\r\n      </dte:DatosEmision>\r\n    </dte:DTE>\r\n    <dte:Adenda>\r\n      <no_interno>A-474211</no_interno>\r\n      <establecimiento>CENTRO FARMACIA</establecimiento>\r\n      <nota_pedido>P. 16414</nota_pedido>\r\n      <vendedor>8</vendedor>\r\n      <forma_pago>CO</forma_pago>\r\n      <dias_credito>0</dias_credito>\r\n      <observaciones>Valor Exento:   Q.143.32</observaciones>\r\n    </dte:Adenda>\r\n  </dte:SAT>\r\n</dte:GTDocumento>";


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
                    string newdata = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><FirmaDocumentoRequest id=\"AA12AA12-B2B2-B3B3-B4B4-AB1000000103\"><xml_dte><![CDATA[<?xml version=\"1.0\" encoding=\"UTF-8\"?><dte:GTDocumento xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:dte=\"http://www.sat.gob.gt/dte/fel/0.2.0\" Version=\"0.1\"><dte:SAT ClaseDocumento=\"dte\"><dte:DTE ID=\"DatosCertificados\"><dte:DatosEmision ID=\"DatosEmision\"><dte:DatosGenerales CodigoMoneda=\"GTQ\" FechaHoraEmision=\"2022-10-09T00:00:00.000-06:00\" Tipo=\"FACT\" /><dte:Emisor AfiliacionIVA=\"GEN\" CodigoEstablecimiento=\"1\" CorreoEmisor=\"jose.alonso@sidgt.com\" NITEmisor=\"109641035\" NombreComercial=\"INVERSIONES MORALES GARCÍA, S.A.\" NombreEmisor=\"CENTRO FARMACIA\"><dte:DireccionEmisor><dte:Direccion>0 AVENIDA 0-11 ZONA 1 LOCAL A</dte:Direccion><dte:CodigoPostal>20022</dte:CodigoPostal><dte:Municipio>EL PROGRESO</dte:Municipio><dte:Departamento>JUTIAPA</dte:Departamento><dte:Pais>GT</dte:Pais></dte:DireccionEmisor></dte:Emisor><dte:Receptor CorreoReceptor=\"jose.alonso@sidgt.com\" IDReceptor=\"CF\" NombreReceptor=\"CONSUMIDOR FINAL\"><dte:DireccionReceptor><dte:Direccion>EL PROGRESO JUTIAPA</dte:Direccion><dte:CodigoPostal>22002</dte:CodigoPostal><dte:Municipio>EL PROGRESO</dte:Municipio><dte:Departamento>JUTIAPA</dte:Departamento><dte:Pais>GT</dte:Pais></dte:DireccionReceptor></dte:Receptor><dte:Frases><dte:Frase CodigoEscenario=\"1\" TipoFrase=\"1\" /><dte:Frase CodigoEscenario=\"9\" TipoFrase=\"4\" /></dte:Frases><dte:Items><dte:Item NumeroLinea=\"1\" BienOServicio=\"B\"><dte:Cantidad>1.0000</dte:Cantidad><dte:UnidadMedida>TAB</dte:UnidadMedida><dte:Descripcion>333.036|ACETAMINOFEN INFASA 500 MG X UNIDAD|INICIAL</dte:Descripcion><dte:PrecioUnitario>0.3000</dte:PrecioUnitario><dte:Precio>0.30</dte:Precio><dte:Descuento>0.00</dte:Descuento><dte:Impuestos><dte:Impuesto><dte:NombreCorto>IVA</dte:NombreCorto><dte:CodigoUnidadGravable>2</dte:CodigoUnidadGravable><dte:MontoGravable>0.30</dte:MontoGravable><dte:MontoImpuesto>0.00</dte:MontoImpuesto></dte:Impuesto></dte:Impuestos><dte:Total>0.30</dte:Total></dte:Item></dte:Items><dte:Totales><dte:TotalImpuestos><dte:TotalImpuesto NombreCorto=\"IVA\" TotalMontoImpuesto=\"0.00\" /></dte:TotalImpuestos><dte:GranTotal>0.30</dte:GranTotal></dte:Totales></dte:DatosEmision></dte:DTE><dte:Adenda><no_interno>A-472810</no_interno><establecimiento>CENTRO FARMACIA</establecimiento><nota_pedido>P. 14990</nota_pedido><vendedor>9</vendedor><forma_pago>CO</forma_pago><dias_credito>0</dias_credito></dte:Adenda></dte:SAT></dte:GTDocumento>]]></xml_dte></FirmaDocumentoRequest>";
                
                    //requestMessage.Headers.Add("Content-Type", "Application/xml");
                   // requestMessage.Headers.Add("Accept", "Application/xml");
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

                                VerificarDocumento verdocto = new();
                                bool VerificacionDoc = await verdocto.VerificacionDocumento(oConfiFel, UuiReferencia);

                                if(VerificacionDoc == true)
                                {
                                    CertificarMP Registar = new CertificarMP();
                                    Registar.RegistrarDocumentoMP(XMLFirmado, oConfiFel,UuidFirmado);
                                }
                                else
                                {

                                }
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
                            }
                           


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

        public string UuidXMLFimardo() => UuidFirmado;
        public string XMLFirmandoDoc() => XMLFirmado;
    }
}
