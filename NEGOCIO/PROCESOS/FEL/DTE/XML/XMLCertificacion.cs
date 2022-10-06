using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.COMPLEMENTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Numerics;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML
{
    public class XMLCertificacion
    {       
     
        // URLs Tag 
        string dte = "http://www.sat.gob.gt/dte/fel/0.2.0";
        string xsi = "http://www.w3.org/2001/XMLSchema-instance";
        string ds = "http://www.w3.org/2000/09/xmldsig#";
        string cex = "http://www.sat.gob.gt/face2/ComplementoExportaciones/0.1.0";
        string cno = "http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0";
        string cfc = "http://www.sat.gob.gt/dte/fel/CompCambiaria/0.1.0";
        string cfe = "http://www.sat.gob.gt/face2/ComplementoFacturaEspecial/0.1.0";

        string URIComplementoNCRE = "http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0";

        public void GenerarXMLCertificacion(long id)
        {
            EstructuraDTE oEstructuraDTE = new EstructuraDTE();
            DatosGeneralesDTE oDatosGeneralesDTE = new DatosGeneralesDTE();
            EmisorDTE oEmisorDTE = new EmisorDTE();
            ReceptorDTE oReceptorDTE = new ReceptorDTE();
            FrasesDTE oFrasesDTE = new FrasesDTE();
            ItemsDTE oItemsDTE = new ItemsDTE();
            ItemsImpuestosDTE oItemsImpuestosDTE = new ItemsImpuestosDTE();          
            TotalesDTE oTotalesDTE = new TotalesDTE();
            AdendasDTE oAdendasDTE = new AdendasDTE();
            FCAMComplemento oFCAMComplemento = new FCAMComplemento();
            NCREComplemento oNCREComplemento = new NCREComplemento();
            FESPComplemento oFESPComplemento = new();



            XmlDocument DocumentoXML = new XmlDocument();
            XmlNode DatosEmision;

            PedidoPv oPedido = new PedidoPv();
            TipoDocumentoFiscal tipoDocto = new();
            using (BIPSContext dbContext = new())
            {
                if(dbContext.PedidoPvs.Any(p => p.Id == id))
                {
                    oPedido = dbContext.PedidoPvs.Where(p => p.Id == id).FirstOrDefault();
                    tipoDocto = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault();
                }
            }

            #region Menu de Seleccion de procesos
            switch (tipoDocto.Nomenclatura.Trim())
            {
                
                case "FACT": //FACTURAS
                    ConstruirXLMFACT();
                    break;

                case "FCAM": // FACTURA CAMBIARIA
                    if (oPedido.LocalOexportacion == false)
                    {
                        ConstruirXLMFCAM(); // FCAM con exportacion
                    }
                    else
                    {
                        ConstruirXLMFCAM(); // FCAM Local
                    }

                    break;

                case "FPEQ": //FACTURAS DE PEQUEÑO CONTRIBUYENTE

                    break;
                case "FCAP":

                    break;
                //FACTURAS ESPECIALES, NOTA DE ABONO Y RECIBO
                case "FESP":
                    ConstruirXLMFESP();
                    
                    break;
                case "NABN":

                    

                    break;
                case "RDON":

                    break;
                case "RECI":

                    break;
                //NOTAS DE CRÉDITO Y DÉBITO
                case "NDEB":
                    ConstruirXLMNDEB();
                    break;
                case "NCRE":
                    ConstruirXLMNCRE();
                    break;
                default:
                    Console.WriteLine("No hay Acronimos a el tipo de factura recibido");
                    break;



            }

            #endregion

            #region Construir XML tipo FACT
            void ConstruirXLMFACT()
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML,dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte,id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);
                
                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACTBIPS.xml");
            }
            #endregion

            #region Construir XML tipo NCRE  
            void ConstruirXLMNCRE() /// Deberan incluirse la frases y escenarios posteriormente
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                //oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oNCREComplemento.ComplementoNCRE(DocumentoXML, dte, id,cno,xsi);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);

                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACTBIPS.xml");
            }
            #endregion

            #region Construir XML tipo NDEB  
            void ConstruirXLMNDEB() /// Deberan incluirse la frases y escenarios posteriormente
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                //oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oNCREComplemento.ComplementoNCRE(DocumentoXML, dte, id, cno, xsi);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);

                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACTBIPS.xml");
            }
            #endregion

            #region Construir XML tipo FCAM  
            void ConstruirXLMFCAM() /// Factura cambiaria tanto local como exportacion
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oFCAMComplemento.FCAMComplementoXML(DocumentoXML, dte, id, xsi,cex,cfc);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);

                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACTBIPS.xml");
            }
            #endregion

            #region Construir XML tipo FESP  
            void ConstruirXLMFESP() /// Factura Especial
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                ///oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id); De Momento no inclye frases
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oFESPComplemento.ComplementoFESPXML(DocumentoXML, dte, id,xsi,cfe);
                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACTBIPS.xml");
            }
            #endregion



            async void ProcesoCertificacionINFILE(string XMLTexto, string Referencia)
            {
                // Conversion a base64
                FirmarINFILE oFirmarINFILE = new FirmarINFILE();
                byte[] XMLByte = Encoding.UTF8.GetBytes(XMLTexto);
                string XMLBase64 = Convert.ToBase64String(XMLByte);

                try
                {
                    var XMLFirmar = new FirmarINFILE()
                    {
                        // NOTA: en la firma se utiliza el Token
                        llave = "1cafce804534d84ae7cbf0bee44e351e",
                        codigo = Referencia,
                        archivo = XMLBase64,
                        alias = "RAICES_DEMO",
                        es_anulacion = "N"
                    };                    
                    await oFirmarINFILE.FirmarDocumento(XMLFirmar);

                }
                catch (Exception e)
                {

                    throw;
                }
                try
                {
                    if(oFirmarINFILE.ResultadoXMLFirmado() == true)
                    {
                        EmisorDTE oEmisor = new EmisorDTE();


                        var ObjCertificar = new CertificarINFILE()
                        {
                            nit_emisor = oEmisor.DatosEmpresariales().Rtu,
                            correo_copia = oEmisor.DatosEmpresariales().CorreoElectronico,
                            xml_dte = oFirmarINFILE.ArchivoXMLFirmado()
                        };

                        CertificarINFILE Certificacion = new();
                        Certificacion.CertificarDocumento(ObjCertificar);
                    }

                }
                catch (Exception)
                {

                    throw;
                }

            }

        }
    }
}
