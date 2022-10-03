using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class EstructuraDTE: NodosInterface
    {
        string dte = "http://www.sat.gob.gt/dte/fel/0.2.0";
        string xsi = "http://www.w3.org/2001/XMLSchema-instance";
        string ds = "http://www.w3.org/2000/09/xmldsig#";
        static XmlNode NodoDatosEminisonXML;
        static XmlNode NodoRefSAT;
      
        public XmlDocument CrearEstructuraXML()
        {
            int prueba;
            // Declaraciones de URLS
            XmlDocument DocXML = new XmlDocument();

            XmlNode GTDocumento = DocXML.CreateElement("dte", "GTDocumento", dte);
            DocXML.AppendChild(GTDocumento);

            //Creacion de Atributos al GTDocumento xmlns:ds xmlns:dte xmlns:xsi  Version  xsi:schemaLocation
            XmlAttribute xmlnsds = DocXML.CreateAttribute("xmlns:ds");
            xmlnsds.Value = ds;
            GTDocumento.Attributes.Append(xmlnsds);

            XmlAttribute xmlnsdte = DocXML.CreateAttribute("xmlns:dte");
            xmlnsdte.Value = dte;
            GTDocumento.Attributes.Append(xmlnsdte);

            /*
            if (true)
            {
                XmlAttribute xmlnsxsi = XML.CreateAttribute("xmlns:xsi");
                xmlnsxsi.Value = xsi;
                GTDocumento.Attributes.Append(xmlnsxsi);

            } */

            XmlAttribute Version = DocXML.CreateAttribute("Version");
            Version.Value = "0.1";
            GTDocumento.Attributes.Append(Version);

            if (true)
            {
                XmlAttribute xsischemaLocation = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
                xsischemaLocation.Value = "http://www.sat.gob.gt/dte/fel/0.2.0";
                GTDocumento.Attributes.Append(xsischemaLocation);

            }
            // Fin de Atributos GTDocumento


            //NODO SAT
            XmlNode SAT = DocXML.CreateElement("dte", "SAT", dte);
            GTDocumento.AppendChild(SAT);
            NodoRefSAT = SAT;

            XmlAttribute ClaseDocumento = DocXML.CreateAttribute("ClaseDocumento");
            ClaseDocumento.Value = "dte";
            SAT.Attributes.Append(ClaseDocumento);

            //NODO DTE
            XmlNode DTE = DocXML.CreateElement("dte", "DTE", dte);
            SAT.AppendChild(DTE);

            XmlAttribute ID = DocXML.CreateAttribute("ID");
            ID.Value = "DatosCertificados";
            DTE.Attributes.Append(ID);

            // NODO DTE __________

            // ****-----
            XmlNode DatosEmision = DocXML.CreateElement("dte", "DatosEmision", dte);
            DTE.AppendChild(DatosEmision);

            XmlAttribute ID2 = DocXML.CreateAttribute("ID");
            ID2.Value = "DatosEmision";
            DatosEmision.Attributes.Append(ID2);
            //----*****
            NodoDatosEminisonXML = DatosEmision;

            
            return DocXML;
        }

        public XmlNode NodoDatosEmision() => NodoDatosEminisonXML;

        public PedidoPv PedidoActual()
        {
            throw new NotImplementedException();
        }

        public XmlNode NodoSAT() => NodoRefSAT;


    }
}
