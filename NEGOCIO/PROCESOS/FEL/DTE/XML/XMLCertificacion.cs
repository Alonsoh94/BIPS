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

        public void GenerarXMLCertificacion()
        {
            XmlDocument DocXML = new XmlDocument();

            #region Generar Encabezado XML Certificacion
            void GenerarEncabezadoXML()
            {
                XmlNode NodoGTDocumento = DocXML.CreateElement("dte", "GTDocumento", dte);
                DocXML.AppendChild(NodoGTDocumento);

                //Creacion de Atributos al NodoGTDocumento xmlns:ds xmlns:dte xmlns:xsi  Version  xsi:schemaLocation
                XmlAttribute xmlnsds = DocXML.CreateAttribute("xmlns:ds");
                xmlnsds.Value = ds;
                NodoGTDocumento.Attributes.Append(xmlnsds);

                XmlAttribute xmlnsdte = DocXML.CreateAttribute("xmlns:dte");
                xmlnsdte.Value = dte;
                NodoGTDocumento.Attributes.Append(xmlnsdte);

                /*
                if (true)
                {
                    XmlAttribute xmlnsxsi = XML.CreateAttribute("xmlns:xsi");
                    xmlnsxsi.Value = xsi;
                    NodoGTDocumento.Attributes.Append(xmlnsxsi);

                } */

                XmlAttribute Version = DocXML.CreateAttribute("Version");
                Version.Value = "0.1";
                NodoGTDocumento.Attributes.Append(Version);

                if (true)
                {
                    XmlAttribute xsischemaLocation = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
                    xsischemaLocation.Value = "http://www.sat.gob.gt/dte/fel/0.2.0";
                    NodoGTDocumento.Attributes.Append(xsischemaLocation);

                }
                // Fin de Atributos NodoGTDocumento


                //NODO SAT
                XmlNode NSAT = DocXML.CreateElement("dte", "SAT", dte);
                NodoGTDocumento.AppendChild(NSAT);
                NodoRefSAT = NSAT;

                XmlAttribute ClaseDocumento = DocXML.CreateAttribute("ClaseDocumento");
                ClaseDocumento.Value = "dte";
                NSAT.Attributes.Append(ClaseDocumento);

                //NODO NDTE
                XmlNode NDTE = DocXML.CreateElement("dte", "DTE", dte);
                NSAT.AppendChild(NDTE);

                XmlAttribute ID = DocXML.CreateAttribute("ID");
                ID.Value = "DatosCertificados";
                NDTE.Attributes.Append(ID);

                // NODO NDTE __________

                // ****-----
                XmlNode NDatosEmision = DocXML.CreateElement("dte", "DatosEmision", dte);
                NDTE.AppendChild(NDatosEmision);

                XmlAttribute ID2 = DocXML.CreateAttribute("ID");
                ID2.Value = "DatosEmision";
                NDatosEmision.Attributes.Append(ID2);
                //----*****

            }
            #endregion
        }
    }
}
