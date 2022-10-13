using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML
{
    public class XMLAnulacion
    {
        public static string? MensajeRequest;
        public static bool ResultadoRequest;
        public async Task<bool> GenerarXMLAnulacion(long Id)
        {
            string dte = "http://www.sat.gob.gt/dte/fel/0.1.0";            
            string ds = "http://www.w3.org/2000/09/xmldsig#";
          //  string nl = "http://www.altova.com/samplexml/other-namespace";

            XmlDocument XmlAnulacion = new XmlDocument();
            Factura? oFactura = new Factura();
            Empresa? oEmpresa = new();
            Establecimiento? oEstablecimiento = new();
            ConfiguracionesFel? oConfiFel = new();
            PedidoPv? oPedido = new();
            DateTime FechaEmisionPedido = new();
            try
            {                
                try
                {
                    using (BIPSContext dbContext = new BIPSContext())
                    {
                        oFactura = dbContext.Facturas.Where(f => f.Id == Id).FirstOrDefault();
                        oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oFactura.Establecimiento).FirstOrDefault();
                        oEmpresa = dbContext.Empresas.Where(e => e.Id == oEstablecimiento.Empresa).FirstOrDefault();
                        oConfiFel = dbContext.ConfiguracionesFels.Where(c => c.Empresa == oEmpresa.Id).FirstOrDefault();
                        FechaEmisionPedido = dbContext.PedidoPvs.Where(p => p.Id == oFactura.PedidoPv).FirstOrDefault().Fecha;
                    }
                }
                catch (Exception e)
                {
                    ResultadoRequest = true;
                    MensajeRequest = "Se ha producido un error al cargar los datos: " + e;
                }


                
                if (oFactura.Certificado == true & oFactura.Anulado == null & oFactura.NumeroAutorizacionC.Trim() != String.Empty)
                {
                    try
                    {
                        XmlNode GTAnulacionDocumento = XmlAnulacion.CreateElement("dte", "GTAnulacionDocumento", dte);
                        XmlAnulacion.AppendChild(GTAnulacionDocumento);

                        XmlDeclaration Declaraciones;
                        Declaraciones = XmlAnulacion.CreateXmlDeclaration("1.0", null, null);
                        Declaraciones.Encoding = "UTF-8";

                        XmlAnulacion.InsertBefore(Declaraciones, GTAnulacionDocumento);

                        //Creacion de Atributos al NodoGTDocumento xmlns:ds xmlns:dte xmlns:xsi Version  xsi: schemaLocation
                        XmlAttribute xmlnsds = XmlAnulacion.CreateAttribute("xmlns:ds");
                        xmlnsds.Value = ds;
                        GTAnulacionDocumento.Attributes.Append(xmlnsds);

                        XmlAttribute xmlnsdte = XmlAnulacion.CreateAttribute("xmlns:dte");
                        xmlnsdte.Value = dte;
                        GTAnulacionDocumento.Attributes.Append(xmlnsdte);

                      // XmlAttribute xmlnsn1 = XmlAnulacion.CreateAttribute("xmlns:n1");
                      // xmlnsn1.Value = nl;
                      // GTAnulacionDocumento.Attributes.Append(xmlnsn1);

                        //  if (DatosFEL.Fel_certificador == "INFILE")
                        //  {
                        //      XmlAttribute xmlnsxsi = XmlAnulacion.CreateAttribute("xmlns:xsi");
                        //      xmlnsxsi.Value = xsi;
                        //      GTAnulacionDocumento.Attributes.Append(xmlnsxsi);
                        //  }


                        XmlAttribute Version = XmlAnulacion.CreateAttribute("Version");
                        Version.Value = "0.1";
                        GTAnulacionDocumento.Attributes.Append(Version);

                        // if (DatosFEL.Fel_certificador == "INFILE")
                        // {
                        //     XmlAttribute xsischemaLocation = XmlAnulacion.CreateAttribute("xsi", "schemaLocation", xsi);
                        //     xsischemaLocation.Value = SL;
                        //     GTAnulacionDocumento.Attributes.Append(xsischemaLocation);
                        //     // Fin de Atributos NodoGTDocumento
                        // }


                        //NODO SAT
                        XmlNode NSAT = XmlAnulacion.CreateElement("dte", "SAT", dte);
                        GTAnulacionDocumento.AppendChild(NSAT);
                        // NOdo AnulacionDTE
                        XmlNode NAnulacionDTE = XmlAnulacion.CreateElement("dte", "AnulacionDTE", dte);
                        NSAT.AppendChild(NAnulacionDTE);

                        XmlAttribute ADatosCertificados = XmlAnulacion.CreateAttribute("ID");
                        ADatosCertificados.Value = "DatosCertificados";
                        NAnulacionDTE.Attributes.Append(ADatosCertificados);

                        // NOdo DatosGenerales
                        XmlNode NDatosGenerales = XmlAnulacion.CreateElement("dte", "DatosGenerales", dte);
                        NAnulacionDTE.AppendChild(NDatosGenerales);
                        //NDatosGenerales.InnerText = String.Empty;

                        //Referencia = Convert.ToString(item[2]);


                        // ++++++++ fecha del Docuamento Original
                        string FechaEmisionDoc = FechaEmisionPedido.ToString("yyyy-MM-ddTHH:mm:ss");
                        FechaEmisionDoc = $"{FechaEmisionDoc}.000-06:00";
                        XmlAttribute NFechaEmisionDocumentoAnular = XmlAnulacion.CreateAttribute("FechaEmisionDocumentoAnular");
                        NFechaEmisionDocumentoAnular.Value = FechaEmisionDoc;
                        NDatosGenerales.Attributes.Append(NFechaEmisionDocumentoAnular);



                        // ++++++ fecha del documento a anular
                        DateTime fechaAnular = DateTime.Now;
                        string FechaAnulacion = fechaAnular.ToString("yyyy-MM-ddTHH:mm:ss");
                        FechaAnulacion = $"{FechaAnulacion}.000-06:00";

                        //string FechaHora2 = oDGA.fecha_hora_anulacion.ToString("yyyy-MM-ddTHH:mm:ss.fffzz:ff");
                        XmlAttribute NFechaHoraAnulacion = XmlAnulacion.CreateAttribute("FechaHoraAnulacion");
                        NFechaHoraAnulacion.Value = FechaAnulacion;
                        NDatosGenerales.Attributes.Append(NFechaHoraAnulacion);

                        XmlAttribute AID = XmlAnulacion.CreateAttribute("ID");
                        AID.Value = "DatosAnulacion";
                        NDatosGenerales.Attributes.Append(AID);

                        XmlAttribute AIDReceptor = XmlAnulacion.CreateAttribute("IDReceptor");
                        AIDReceptor.Value = Convert.ToString(oFactura.Nit);
                        NDatosGenerales.Attributes.Append(AIDReceptor);

                        string Variable = "Motivo de Anulacion";

                        XmlAttribute AMotivoAnulacion = XmlAnulacion.CreateAttribute("MotivoAnulacion");
                        AMotivoAnulacion.Value = Convert.ToString(Variable);
                        NDatosGenerales.Attributes.Append(AMotivoAnulacion);

                        XmlAttribute ANITEmisor = XmlAnulacion.CreateAttribute("NITEmisor");
                        ANITEmisor.Value = Convert.ToString(oEmpresa.Rtu.Trim());
                        NDatosGenerales.Attributes.Append(ANITEmisor);

                        // NitEmisor = Convert.ToString(item[8]);

                        XmlAttribute ANumeroDocumentoAAnular = XmlAnulacion.CreateAttribute("NumeroDocumentoAAnular");
                        ANumeroDocumentoAAnular.Value = Convert.ToString(oFactura.NumeroAutorizacionC.Trim());
                        NDatosGenerales.Attributes.Append(ANumeroDocumentoAAnular);

                        XmlAnulacion.Save(oConfiFel.PathXml + @"\AnularBips.xml");

                        ResultadoRequest = true;
                        MensajeRequest = "Exito";

                    }
                    catch (Exception e)
                    {
                        ResultadoRequest = false;
                        MensajeRequest = "Se ha producido un error al intentar generar el XML para anulaciones: " + e.Message;
                    }


                }
                else if (oFactura.Anulado == true)
                {
                    ResultadoRequest = false;
                    MensajeRequest = "El Documento tiene un Estado Anulado... No se puede procesar nuevamente...";
                }
                else
                {
                    ResultadoRequest = false;
                    MensajeRequest = "No se puede procesar su solicitud... Asegurese que los datos esten correctos...";

                }                

            }
            catch (Exception e)
            {
                ResultadoRequest = false;
                MensajeRequest = "Se ha producido un error durante el proceso de Generacion Anulación de Documentos..." + e.Message;
            }
            try
            {
                if (ResultadoRequest == true)
                {
                    if (oConfiFel.Certificador.Trim() == "INFILE")
                    {
                        byte[] XMLByte = Encoding.UTF8.GetBytes(XmlAnulacion.InnerXml);
                        string XMLBase64 = Convert.ToBase64String(XMLByte);
                        FirmarINFILE oFirmarInfile = new();
                        var ObjFirmar = new FirmarINFILE()
                        {
                            llave = oConfiFel.Token.Trim(), // En la firma se utiliza el token y el centificacion o nauladion la llave o key
                            codigo = oFactura.ReferenciaInterna,
                            archivo = XMLBase64,
                            alias = oConfiFel.Usuario.Trim(),
                            es_anulacion = "S"
                        };
                        bool ResultadoFirma = await oFirmarInfile.FirmarDocumento(ObjFirmar, oConfiFel);

                        if (ResultadoFirma == true)
                        {
                            string EmailCopy;
                            if(oConfiFel.CorreoCopia == null)
                            {
                                EmailCopy = string.Empty;
                            }
                            else
                            {
                                EmailCopy = oConfiFel.CorreoCopia.ToString();
                            }
                            AnularINFILE oAnularInfile = new AnularINFILE();
                            var ObjAnular = new AnularINFILE()
                            {
                                nit_emisor = oEmpresa.Rtu.Trim(),
                                correo_copia = EmailCopy,
                                xml_dte = oFirmarInfile.ArchivoXMLFirmado()
                            };
                            oAnularInfile.AnularDocumento(ObjAnular, oConfiFel, oFactura.ReferenciaInterna, oFactura.Id);
                        }


                    }
                    else if (oConfiFel.Certificador.Trim() == "MEGAPRINT")
                    {

                    }
                }
            }
            catch (Exception e)
            {
                MensajeRequest = "Error: " + e.Message;
            }
            

            return ResultadoRequest;


        }
      
    }
}
