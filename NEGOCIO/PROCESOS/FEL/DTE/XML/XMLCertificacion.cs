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
using Microsoft.VisualBasic.ApplicationServices;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        //static ConfiguracionesFel oConfiFel = new();


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
            Establecimiento oEstablecimiento = new();
            ConfiguracionesFel oConfiFel = new();
            Cliente oCliente = new Cliente();

            using (BIPSContext dbContext = new())
            {
                if (dbContext.PedidoPvs.Any(p => p.Id == id))
                {
                    oPedido = dbContext.PedidoPvs.Where(p => p.Id == id).FirstOrDefault();
                    tipoDocto = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault();
                    oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oPedido.Establecimiento).FirstOrDefault();
                    oConfiFel = dbContext.ConfiguracionesFels.Where(c => c.Empresa == oEstablecimiento.Empresa).FirstOrDefault();
                    oCliente = dbContext.Clientes.Where(c => c.Id == oPedido.Cliente).FirstOrDefault();

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

                if (oConfiFel.Certificador == "INFILE")
                {
                    ProcesoCertificacionINFILE(DocumentoXML.OuterXml);
                }
                else if (oConfiFel.Certificador == "MEGAPRINT")
                {
                    ProcesoCertificacionMEGAPRINT(DocumentoXML);
                }
                DocumentoXML.Save(@$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml");
            }
            #endregion

            #region Construir XML tipo NCRE  
            void ConstruirXLMNCRE() /// Deberan incluirse la frases y escenarios posteriormente
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oNCREComplemento.ComplementoNCRE(DocumentoXML, dte, id,cno,xsi);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);

                if (oConfiFel.Certificador == "INFILE")
                {
                    ProcesoCertificacionINFILE(DocumentoXML.OuterXml);
                }
                else if (oConfiFel.Certificador == "MEGAPRINT")
                {
                    ProcesoCertificacionMEGAPRINT(DocumentoXML);
                }
                DocumentoXML.Save(@$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml");
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

                if (oConfiFel.Certificador == "INFILE")
                {
                    ProcesoCertificacionINFILE(DocumentoXML.OuterXml);
                }
                else if (oConfiFel.Certificador == "MEGAPRINT")
                {
                    ProcesoCertificacionMEGAPRINT(DocumentoXML);
                }
                DocumentoXML.Save(@$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml");
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

                if(oConfiFel.Certificador == "INFILE")
                {
                    ProcesoCertificacionINFILE(DocumentoXML.OuterXml);
                }
                else if(oConfiFel.Certificador == "MEGAPRINT")
                {
                    ProcesoCertificacionMEGAPRINT(DocumentoXML);
                }
                
                DocumentoXML.Save(@$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml");
                

            
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
                if (oConfiFel.Certificador == "INFILE")
                {
                    ProcesoCertificacionINFILE(DocumentoXML.OuterXml);
                }
                else if (oConfiFel.Certificador == "MEGAPRINT")
                {
                    ProcesoCertificacionMEGAPRINT(DocumentoXML);
                }
                DocumentoXML.Save(@$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml");
            }
            #endregion



            async void ProcesoCertificacionINFILE(string XMLTexto)
            {
                
                bool ResultadoFirma = false;
                bool ResultadoCertificacion = false;
                // Conversion a base64
                FirmarINFILE oFirmarINFILE = new FirmarINFILE();
                CertificarINFILE Certificacion = new();
                ImprimirInfile oImprimirInfile = new ImprimirInfile();
                byte[] XMLByte = Encoding.UTF8.GetBytes(XMLTexto);
                string XMLBase64 = Convert.ToBase64String(XMLByte);

                try
                {
                    var XMLFirmar = new FirmarINFILE()
                    {
                        // NOTA: en la firma se utiliza el Token
                        llave = oConfiFel.Token.Trim(),
                        codigo = oPedido.ReferenciaInterna.Trim(),
                        archivo = XMLBase64,
                        alias = oConfiFel.Usuario.Trim(),
                        es_anulacion = "N"
                    };                    
                    ResultadoFirma = await oFirmarINFILE.FirmarDocumento(XMLFirmar,oConfiFel);

                }
                catch (Exception e)
                {

                    throw;
                }
                try
                {
                    if(ResultadoFirma == true)
                    {
                        EmisorDTE oEmisor = new EmisorDTE();
                        GenerarGUID oGenerarGUID = new();


                        var ObjCertificar = new CertificarINFILE()
                        {
                            nit_emisor = oEmisor.DatosEmpresariales().Rtu,
                            correo_copia = oEmisor.DatosEmpresariales().CorreoElectronico,
                            xml_dte = oFirmarINFILE.ArchivoXMLFirmado()
                        };

                        
                       ResultadoCertificacion = await Certificacion.CertificarDocumento(ObjCertificar, oConfiFel, oPedido.ReferenciaInterna.Trim());
                       
                       
                    }

                    if (ResultadoCertificacion == true)
                    {
                        await GenerarFactura();

                        CertificarINFILE oCertificarINFILE = new();
                        ResponseOK Certificado = oCertificarINFILE.MiCertificacion();
                        string sUrl = oConfiFel.UrlimprimirInfile.Trim() + Certificado.uuid.Trim();
                        string sPath = oConfiFel.PathPdfgenerado.Trim() + "\\" + oPedido.ReferenciaInterna.Trim() + ".pdf";
                        await oImprimirInfile.ReimprimirFactura(sUrl, sPath);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            async void ProcesoCertificacionMEGAPRINT(XmlDocument DoctoXML)
            {
                FirmarMP oFirmarMP = new FirmarMP();
               // byte[] XMLByte = Encoding.UTF8.GetBytes(XMLTexto);
               // string XMLBase64 = Convert.ToBase64String(XMLByte);
                bool ResultadoFirma;

                DateTime hoy = DateTime.Now;
                if( hoy <= oConfiFel.ExpiraToken )
                {
                    ResultadoFirma = await oFirmarMP.FirmarDocumento(DoctoXML, oPedido.ReferenciaInterna.Trim(), oConfiFel);
                }
                else
                {
                    RequestTokenMP oRequestTokenMP = new();

                    if (await oRequestTokenMP.RequestToken(oConfiFel))
                    {


                    }
                }

            }

            async Task GenerarFactura()
            {
                CertificarINFILE oCertificarINFILE = new();
                ResponseOK Certificado = oCertificarINFILE.MiCertificacion();
                decimal calculo = (oPedido.TotalPedido / 1.12m);
                double ValorIva = 10.71;
                using (BIPSContext dbContext = new BIPSContext())
                {
                    Factura Fac = new Factura();
                    Fac.ReferenciaInterna = oPedido.ReferenciaInterna.Trim();
                    Fac.Establecimiento = oEstablecimiento.Id;
                    Fac.TipoCargoCxc = 1;
                    Fac.Cliente = oCliente.Id;
                    Fac.Nit = oCliente.Nit.Trim();
                    Fac.Vendedor = 1;
                    Fac.PedidoPv = oPedido.Id;
                    Fac.FechaFacturacion = DateTime.Now;
                    Fac.Moneda = 1;
                    Fac.TipoCambio = 7.90m;
                    Fac.ValorTotal = oPedido.TotalPedido;
                    Fac.ValorIva = Convert.ToDecimal(ValorIva);
                    Fac.Certificado = true;
                    Fac.NumeroAutorizacionC = Certificado.uuid.Trim();
                    Fac.SerieC = Certificado.serie.Trim();
                    Fac.NumeroDoctoC = Certificado.numero.Trim();
                    Fac.FechaCertificado = DateTime.Now;
                    Fac.EstadoGeneral = true;

                    dbContext.Facturas.Add(Fac);
                    dbContext.SaveChanges();

                }

            }

        }

        
    }
}
