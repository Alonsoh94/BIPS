using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FACTURACION.IMPLEMENTACION;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.MEGAPRINT;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.COMPLEMENTOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML
{
    public class XMLCertificacion
    {
        // Declaracion de Contenedores de Datos
        public static PedidoPv? oPedido;
        public static TipoDocumentoFiscal? oTipoDoctoFiscal;
        public static Establecimiento? oEstablecimiento;
        public static ConfiguracionesFel? oConfiFel;
        public static Cliente? oCliente;       
        public static Empresa? oEmpresa;
        public static Monedum? oMoneda;
        public static Municipio oMunicipioEmisor;
        public static Departamento oDepartamentoEmisor;
        public static Paise oPaisEmisor;
        public static Municipio oMunicipioCliente;
        public static Departamento oDepartamentoCliente;
        public static Paise oPaisCliente;


        public long id;
        public string CodigoRef;



        public static XmlDocument DocumentoGenerado;
        public static XmlDocument DocumentoXML;

        // Valiables de informacion
        public static string Mensaje;
        public static bool Resultado;
        string Ruta = string.Empty;


        // URLs Tag 
        string dte = "http://www.sat.gob.gt/dte/fel/0.2.0";
        string xsi = "http://www.w3.org/2001/XMLSchema-instance";
        string ds = "http://www.w3.org/2000/09/xmldsig#";
        string cex = "http://www.sat.gob.gt/face2/ComplementoExportaciones/0.1.0";
        string cno = "http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0";
        string cfc = "http://www.sat.gob.gt/dte/fel/CompCambiaria/0.1.0";
        string cfe = "http://www.sat.gob.gt/face2/ComplementoFacturaEspecial/0.1.0";
        string URIComplementoNCRE = "http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0";

        // instacias de Clases
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
        NDEBComplemento oNDEBComplemento = new NDEBComplemento();



        // Funcion principal para Construir XML
        public async Task<XmlDocument> GenerarXMLCertificacion(long DTEid)
        {
            id = DTEid;

            // instacia del Documento principal
            DocumentoXML = new XmlDocument();

            try
            {
                using (BIPSContext dbContext = new())
                {
                    if (dbContext.PedidoPvs.Any(p => p.Id == id))
                    {
                        oPedido = dbContext.PedidoPvs.Where(p => p.Id == id).FirstOrDefault();
                        oTipoDoctoFiscal = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault();
                        oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oPedido.Establecimiento).FirstOrDefault();
                        oConfiFel = dbContext.ConfiguracionesFels.Where(c => c.Empresa == oEstablecimiento.Empresa).FirstOrDefault();
                        oCliente = dbContext.Clientes.Where(c => c.Id == oPedido.Cliente).FirstOrDefault();
                        oEmpresa = dbContext.Empresas.Where(e => e.Id == oEstablecimiento.Empresa).FirstOrDefault();
                        oMoneda = dbContext.Moneda.Where(m => m.Id == oEmpresa.MonedaBase).FirstOrDefault();
                        oMunicipioEmisor = dbContext.Municipios.Where(m => m.Id == oEstablecimiento.Municipio).FirstOrDefault();
                        oDepartamentoEmisor = dbContext.Departamentos.Where(d => d.Id == oMunicipioEmisor.Departamento).FirstOrDefault();
                        oPaisEmisor = dbContext.Paises.Where(p => p.Id == oDepartamentoEmisor.Pais).FirstOrDefault();
                        oMunicipioCliente = dbContext.Municipios.Where(m => m.Id == oCliente.Municipio).FirstOrDefault();
                        oDepartamentoCliente = dbContext.Departamentos.Where(d => d.Id == oMunicipioCliente.Departamento).FirstOrDefault();
                        oPaisCliente = dbContext.Paises.Where(p => p.Id == oDepartamentoCliente.Pais).FirstOrDefault();
                        oMunicipioCliente = dbContext.Municipios.Where(m => m.Id == oCliente.Municipio).FirstOrDefault();
                        oDepartamentoCliente = dbContext.Departamentos.Where(d => d.Id == oMunicipioCliente.Departamento).FirstOrDefault();
                        oPaisCliente = dbContext.Paises.Where(p => p.Id == oDepartamentoCliente.Pais).FirstOrDefault();
                    }

                }

            }
            catch (Exception e)
            {
                Resultado = true;
                Mensaje = "Error al cargar la información requerida de la base de datos... " + e;
            }


            CodigoRef = $"{oTipoDoctoFiscal.Nomenclatura.Trim()}-{oPedido.Id}";
            Ruta = @$"{oConfiFel.PathXml.Trim()}\FACTBIPS.xml";
            #region Menu de Seleccion de procesos
            switch (oTipoDoctoFiscal.Nomenclatura.Trim())
            {

                case "FACT": //FACTURAS
                    await ConstruirXLMFACT();
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
                   await ConstruirXLMFESP();

                    break;
                case "NABN":



                    break;
                case "RDON":

                    break;
                case "RECI":

                    break;
                //NOTAS DE CRÉDITO Y DÉBITO
                case "NDEB":
                    await ConstruirXLMNDEB();
                    break;
                case "NCRE":
                   await ConstruirXLMNCRE();
                    break;
                default:
                    Console.WriteLine("No hay Acronimos a el tipo de factura recibido");
                    break;
            }

            #endregion
            return DocumentoXML;
        }

        #region CONSTRUCCIONS DE XML FACT
        public async Task<XmlDocument> ConstruirXLMFACT()
        {
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte,ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML,oPedido,oEstablecimiento,oEmpresa,oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML,oPedido,oEstablecimiento,oMunicipioEmisor,oDepartamentoEmisor,oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML,oCliente,oMunicipioCliente,oDepartamentoCliente,oPaisCliente, dte, id);
            oFrasesDTE.ModuloFrasesDTE(DocumentoXML,oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML,oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML,oTipoDoctoFiscal, oPedido, dte, id);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);
            try
            {
                DocumentoXML.Save(Ruta);
            }
            catch (Exception)
            {
                Mensaje = "Error al Guardar el XML en Disco, Revise su PathXML";
            }           
            
            return DocumentoXML;
        }
        #endregion

        #region Construir XML tipo NCRE  
        async Task<XmlDocument> ConstruirXLMNCRE() /// Deberan incluirse la frases y escenarios posteriormente
        {           
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte, ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, oPedido, oEstablecimiento, oEmpresa, oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML, oPedido, oEstablecimiento, oMunicipioEmisor, oDepartamentoEmisor, oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML, oCliente, oMunicipioCliente, oDepartamentoCliente, oPaisCliente, dte, id);
           // oFrasesDTE.ModuloFrasesDTE(DocumentoXML, oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML,oTipoDoctoFiscal, oPedido, dte, id);
            oNCREComplemento.ComplementoNCRE(DocumentoXML, dte, id,cno,xsi);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);
                        
            DocumentoXML.Save(Ruta);
            return DocumentoXML;
        }
        #endregion

        #region Construir XML tipo NDEB  
        async Task<XmlDocument> ConstruirXLMNDEB() /// Deberan incluirse la frases y escenarios posteriormente
        {
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte, ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, oPedido, oEstablecimiento, oEmpresa, oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML, oPedido, oEstablecimiento, oMunicipioEmisor, oDepartamentoEmisor, oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML, oCliente, oMunicipioCliente, oDepartamentoCliente, oPaisCliente, dte, id);
            // oFrasesDTE.ModuloFrasesDTE(DocumentoXML, oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML,oTipoDoctoFiscal, oPedido, dte, id);
            oNDEBComplemento.ComplementoNDEB(DocumentoXML, dte, id, cno, xsi);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);

            DocumentoXML.Save(Ruta);
            return DocumentoXML;
        }
        #endregion

        #region Construir XML tipo FCAM  
        async Task<XmlDocument> ConstruirXLMFCAM() /// Factura cambiaria tanto local como exportacion
        {
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte, ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, oPedido, oEstablecimiento, oEmpresa, oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML, oPedido, oEstablecimiento, oMunicipioEmisor, oDepartamentoEmisor, oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML, oCliente, oMunicipioCliente, oDepartamentoCliente, oPaisCliente, dte, id);
            oFrasesDTE.ModuloFrasesDTE(DocumentoXML, oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML,oTipoDoctoFiscal, oPedido, dte, id);
            oFCAMComplemento.FCAMComplementoXML(DocumentoXML, oPedido, dte, id, xsi,cex,cfc);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);

            DocumentoXML.Save(Ruta);
            return DocumentoXML;
        }
        #endregion

        #region Construir XML tipo FESP  
        async Task<XmlDocument> ConstruirXLMFESP() /// Factura Especial
        {
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte, ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, oPedido, oEstablecimiento, oEmpresa, oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML, oPedido, oEstablecimiento, oMunicipioEmisor, oDepartamentoEmisor, oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML, oCliente, oMunicipioCliente, oDepartamentoCliente, oPaisCliente, dte, id);
            //oFrasesDTE.ModuloFrasesDTE(DocumentoXML, oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oFESPComplemento.ComplementoFESPXML(DocumentoXML,oPedido, dte, id,xsi,cfe);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);

            DocumentoXML.Save(Ruta);
            return DocumentoXML;
        }
        #endregion
        #region Construir XML tipo NABN 
        async Task<XmlDocument> ConstruirXLMNABN() /// Deberan incluirse la frases y escenarios posteriormente
        {
            DocumentoXML = oEstructuraDTE.CrearEstructuraXML(dte, ds);
            oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, oPedido, oEstablecimiento, oEmpresa, oMoneda, oTipoDoctoFiscal, dte, id);
            oEmisorDTE.ModuloEmisorDTE(DocumentoXML, oPedido, oEstablecimiento, oMunicipioEmisor, oDepartamentoEmisor, oPaisEmisor, oEmpresa, dte, id);
            oReceptorDTE.ModuloReceptorDTE(DocumentoXML, oCliente, oMunicipioCliente, oDepartamentoCliente, oPaisCliente, dte, id);
            // oFrasesDTE.ModuloFrasesDTE(DocumentoXML, oTipoDoctoFiscal, dte, id);
            oItemsDTE.ModuloItemsDTE(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oTotalesDTE.ModuloTotales(DocumentoXML, oTipoDoctoFiscal, oPedido, dte, id);
            oNCREComplemento.ComplementoNCRE(DocumentoXML, dte, id, cno, xsi);
            DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, oEmpresa, dte, id);

            DocumentoXML.Save(Ruta);
            return DocumentoXML;
        }
        #endregion


        #region PROCEDIMIENTO PARA FIRMAR DTE
        //************ area de certificacion
        public async Task<bool> FirmarDocumento()
        {
            bool ResultadoFirma = false;
            if(oConfiFel.Certificador == "INFILE")
            {
                byte[] XMLByte = Encoding.UTF8.GetBytes(DocumentoXML.OuterXml);
                string XMLBase64 = Convert.ToBase64String(XMLByte);

                FirmarINFILE oFirma = new();
                var obj = new FirmarINFILE()
                {
                    // NOTA: en la firma se utiliza el Token
                    llave = oConfiFel.Token.Trim(),
                    codigo = oPedido.ReferenciaInterna.Trim(),
                    archivo = XMLBase64,
                    alias = oConfiFel.Usuario.Trim(),
                    es_anulacion = "N"
                };
                ResultadoFirma = await  oFirma.FirmarDocumento(obj,oConfiFel);

            }
            else if(oConfiFel.Certificador == "MEGAPRIN")
            {
                FirmarMP oFirmarMP = new FirmarMP();                
                bool ResultadoCertificacion = false;
                VerificarDocumento verdocto = new();

                try
                {
                    DateTime hoy = DateTime.Now;
                    if (hoy <= oConfiFel.ExpiraToken)
                    {
                        if (await verdocto.VerificacionDocumento(oConfiFel, oPedido.ReferenciaInterna.Trim()))
                        {
                            if (await oFirmarMP.FirmarDocumento(DocumentoXML.OuterXml.ToString(), oPedido.ReferenciaInterna.Trim(), oConfiFel))
                            {
                                ResultadoFirma = true;
                            }
                            else
                            {
                                Mensaje = oFirmarMP.MensajeResultado();
                                ResultadoFirma = false;
                            }
                        }
                        else
                        {
                            ResultadoFirma = false;
                            Mensaje = verdocto.ResultadoVerificacionDocto();
                        }                        

                    }
                    else
                    {
                        RequestTokenMP oRequestTokenMP = new();
                       // VerificarDocumento verdocto = new();

                        if (await oRequestTokenMP.RequestToken(oConfiFel))
                        {
                            if (await verdocto.VerificacionDocumento(oConfiFel, oPedido.ReferenciaInterna.Trim()))
                            {
                                if (await oFirmarMP.FirmarDocumento(DocumentoXML.OuterXml.ToString(), oPedido.ReferenciaInterna.Trim(), oConfiFel))
                                {
                                    ResultadoFirma = true;
                                }
                                else
                                {
                                    Mensaje = oFirmarMP.MensajeResultado();
                                    ResultadoFirma = false;
                                }
                            }
                            else
                            {
                                ResultadoFirma = false;
                                Mensaje = verdocto.ResultadoVerificacionDocto();
                            }
                        }
                        else
                        {
                            ResultadoCertificacion = false;
                            Mensaje = oRequestTokenMP.MensajeSolicitudToken();
                        }
                    }
                }
                catch (Exception e)
                {
                    ResultadoCertificacion = false;
                    Mensaje = "Se ha producido un Error: " + e.Message;
                }

            }
            return ResultadoFirma;
        }
        #endregion

        #region PROCEDIMIENTO PARA CERTIFIAR DTE
        public async Task<bool> CertificarDocumento()
        {
            bool ResultadoCertificacion = false;
            if (oConfiFel.Certificador == "INFILE")
            {
                FirmarINFILE oFirmarInfile = new FirmarINFILE();
                CertificarINFILE Certificacion = new CertificarINFILE();
                var ObjCertificar = new CertificarINFILE()
                {
                    nit_emisor = oEmpresa.Rtu.Trim(),
                    correo_copia = oEmpresa.CorreoElectronico.Trim(),
                    xml_dte = oFirmarInfile.ArchivoXMLFirmado()
                };
                ResultadoCertificacion = await Certificacion.CertificarDocumento(ObjCertificar, oConfiFel, oPedido.ReferenciaInterna.Trim());

            }
            else if (oConfiFel.Certificador == "MEGAPRIN")
            {
                FirmarMP oFirmarMP = new FirmarMP();    
                CertificarMP Registar = new CertificarMP();
                ResultadoCertificacion = await Registar.RegistrarDocumentoMP(oFirmarMP.XMLFirmandoDoc(), oConfiFel, oPedido.ReferenciaInterna.Trim());

            }
            return ResultadoCertificacion;
        }
        #endregion

        #region PROCEDIMIENTO PARA IMPRIMIR LA FACTURA
        public async Task<bool> ImprimirFactura()
        {
            bool Resultadoimpresion = false;
            if (oConfiFel.Certificador == "INFILE")
            {
                CertificarINFILE oCertificacion = new CertificarINFILE();
                ResponseOK res = oCertificacion.MiCertificacion();
                ImprimirInfile imprimir = new ImprimirInfile();
                Resultadoimpresion = await imprimir.ImprimirFactura(oConfiFel.UrlimprimirInfile.Trim() + res.uuid.Trim(), oConfiFel.PathPdfgenerado+@"\"+ CodigoRef + ".pdf");
            }
            else if (oConfiFel.Certificador == "MEGAPRIN")
            {
                CertificarMP cert = new CertificarMP();
                ObtenerPDFMP oPDF = new ObtenerPDFMP();
                oPDF.ImpresionFactura(oConfiFel, cert.UuidCertificado());

            }
            return Resultadoimpresion;
        }
        #endregion

        #region PROCEDIMIENTO PARA GENERAR FACTURA EN EL SISTEMA
        public async Task<bool> Facturar()
        {
            GenerarFactura Factu = new();
            bool Resultado = false;
            if (oConfiFel.Certificador == "INFILE")
            {
                CertificarINFILE cer = new CertificarINFILE();                
                ResponseOK oResponse = cer.MiCertificacion();
                Resultado = await Factu.GenerarcionFactura(oPedido, oEstablecimiento, oCliente, oResponse);
                if (Resultado == false)
                {
                    Mensaje = Factu.MensajeResultado();
                }
            }
            else if (oConfiFel.Certificador == "MEGAPRIN")
            {
                CertificarMP oCerti = new CertificarMP();
                ResponseOK Resp = oCerti.MiCertificacion();
                Resultado = await Factu.GenerarcionFactura(oPedido, oEstablecimiento, oCliente, Resp);
                if (Resultado == false)
                {
                    Mensaje = Factu.MensajeResultado();
                }
            }
            return Resultado;
        }
        #endregion

        public string MensajeCertificacion() => Mensaje;
    }
}
