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

        string URIComplementoNCRE = "http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0";

        public void GenerarXMLCertificacion(int id)
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



            XmlDocument DocumentoXML = new XmlDocument();
            XmlNode DatosEmision;

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
                //DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACT.xml");
            }
            #endregion

            #region Construir XML tipo FCAM
            void ConstruirXLMFCAM()
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML, dte, id);
                oEmisorDTE.ModuloEmisorDTE(DocumentoXML, dte, id);
                oReceptorDTE.ModuloReceptorDTE(DocumentoXML, dte, id);
                oFrasesDTE.ModuloFrasesDTE(DocumentoXML, dte, id);
                oItemsDTE.ModuloItemsDTE(DocumentoXML, dte, id);
                oTotalesDTE.ModuloTotales(DocumentoXML, dte, id);
                oFCAMComplemento.FCAMComplementoXML(DocumentoXML, dte, id,xsi,cex);
                DocumentoXML = oAdendasDTE.ModuloAdendasDTE(DocumentoXML, dte, id);

                ProcesoCertificacionINFILE(DocumentoXML.OuterXml, oDatosGeneralesDTE.CodigoReferencia());
                //DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\XML\FACT.xml");
            }
            #endregion

            ConstruirXLMFACT();

            async void ProcesoCertificacionINFILE(string XMLTexto, string Referencia)
            {
                // Conversion a base64

                byte[] XMLByte = Encoding.UTF8.GetBytes(XMLTexto);
                string XMLBase64 = Convert.ToBase64String(XMLByte);

                var XMLFirmar = new FirmarINFILE()
                {
                    // NOTA: en la firma se utiliza el Token
                    llave = "1cafce804534d84ae7cbf0bee44e351e",
                    codigo = Referencia,
                    archivo = XMLBase64,
                    alias = "RAICES_DEMO",
                    es_anulacion = "N"
                };

                FirmarINFILE oFirmarINFILE = new FirmarINFILE();
                await oFirmarINFILE.FirmarDocumento(XMLFirmar);

                    

            }
        }
    }
}
