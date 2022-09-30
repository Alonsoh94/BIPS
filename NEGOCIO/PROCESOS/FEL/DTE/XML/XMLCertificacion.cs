using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
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
        BIPSContext Contexto = new BIPSContext();

     
        // URLs Tag 
        string dte = "http://www.sat.gob.gt/dte/fel/0.2.0";
        string xsi = "http://www.w3.org/2001/XMLSchema-instance";
        string ds = "http://www.w3.org/2000/09/xmldsig#";

        public void GenerarXMLCertificacion()
        {
            EstructuraDTE oEstructuraDTE = new EstructuraDTE();
            DatosGeneralesDTE oDatosGeneralesDTE = new DatosGeneralesDTE(Contexto);
            EmisorDTE oEmisorDTE = new EmisorDTE();
            ReceptorDTE oReceptorDTE = new ReceptorDTE();
            FrasesDTE oFrasesDTE = new FrasesDTE();
            ItemsDTE oItemsDTE = new ItemsDTE();
            ItemsImpuestosDTE oItemsImpuestosDTE = new ItemsImpuestosDTE();
            ImpuestosDTE oImpuestosDTE = new ImpuestosDTE();
            TotalesDTE oTotalesDTE = new TotalesDTE();
            AdendasDTE oAdendasDTE = new AdendasDTE();



            XmlDocument DocumentoXML = new XmlDocument();
            XmlNode DatosEmision;

            #region Generar Encabezado XML Certificacion
            void ConstruirXLMFACT()
            {
                DocumentoXML = oEstructuraDTE.CrearEstructuraXML();
                DocumentoXML = oDatosGeneralesDTE.ModuloDatosGenerales(DocumentoXML,dte);
              

                DocumentoXML.Save(@"C:\Users\Jose Alonso\Documents\MISXML\BipsFACT.xml");
            }
            #endregion

            ConstruirXLMFACT();
        }
    }
}
