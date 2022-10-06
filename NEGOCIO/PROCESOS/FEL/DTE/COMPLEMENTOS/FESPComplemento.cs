using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.COMPLEMENTOS
{
    public class FESPComplemento
    {
        ComplementoFesp oComplementoFesp;

        PedidoPv oPedido;
        XmlNode DatosEmision;
        BIPSContext dbContext;
        public XmlDocument ComplementoFESPXML(XmlDocument DocXML, string dte, long Id, string xsi, string cfe)
        {

            NodosInterface nodos = new EstructuraDTE();
            NodosInterface DatosGenerales = new DatosGeneralesDTE();
            DatosEmision = nodos.NodoDatosEmision();
            oPedido = DatosGenerales.PedidoActual();

            XmlNode NComplementos = DocXML.CreateElement("dte", "Complementos", dte);
            DatosEmision.AppendChild(NComplementos);

            using (dbContext = new BIPSContext())
            {
                if (dbContext.ComplementoFesps.Any(e => e.PedidoPv == oPedido.Id))
                {
                    oComplementoFesp = dbContext.ComplementoFesps.Where(e => e.PedidoPv == oPedido.Id).FirstOrDefault();
                }
            }

            XmlNode NodoComplemento = DocXML.CreateElement("dte", "Complemento", dte);
            NComplementos.AppendChild(NodoComplemento);

            XmlAttribute AIDComplemento = DocXML.CreateAttribute("IDComplemento");
            AIDComplemento.Value = Convert.ToString(oComplementoFesp.Idcomplemento);
            NodoComplemento.Attributes.Append(AIDComplemento);

            XmlAttribute ANombreComplemento = DocXML.CreateAttribute("NombreComplemento");
            ANombreComplemento.Value = Convert.ToString(oComplementoFesp.NombreComplemento.Trim());
            NodoComplemento.Attributes.Append(ANombreComplemento);

            XmlAttribute URIComplemento = DocXML.CreateAttribute("URIComplemento");
            URIComplemento.Value = Convert.ToString(oComplementoFesp.Uricomplemento.Trim());
            NodoComplemento.Attributes.Append(URIComplemento);

            //NODO ABONO FACTURA CAMBIARIA

            XmlNode NRetencionesFacturaEspecial = DocXML.CreateElement("cfe", "RetencionesFacturaEspecial", "cfe");
            NodoComplemento.AppendChild(NRetencionesFacturaEspecial);

            XmlAttribute Nxmlnscfc = DocXML.CreateAttribute("xmlns:cfe");
            Nxmlnscfc.Value = cfe;
            NRetencionesFacturaEspecial.Attributes.Append(Nxmlnscfc);

            XmlAttribute NVersionFEsp = DocXML.CreateAttribute("Version");
            NVersionFEsp.Value = oComplementoFesp.Version.Trim();
            NRetencionesFacturaEspecial.Attributes.Append(NVersionFEsp);

            if ("++++++++" == "INFILE")
            {
                XmlAttribute AschemaLocationEsp = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
                AschemaLocationEsp.Value = @"http://www.sat.gob.gt/face2/ComplementoFacturaEspecial/0.1.0 C:\Users\User\Desktop\FEL\Esquemas\GT_Complemento_Fac_Especial-0.1.0.xsd";
                NRetencionesFacturaEspecial.Attributes.Append(AschemaLocationEsp);
            }


            //NODO ABONO
            XmlNode NRetencionISR = DocXML.CreateElement("cfe", "RetencionISR", cfe); // Nodo ISR
            NRetencionesFacturaEspecial.AppendChild(NRetencionISR);
            NRetencionISR.InnerText = Convert.ToString(oComplementoFesp.RetencionIsr);

            XmlNode NRetencionIVA = DocXML.CreateElement("cfe", "RetencionIVA", cfe);  //NODO Iva
            NRetencionesFacturaEspecial.AppendChild(NRetencionIVA);
            NRetencionIVA.InnerText = Convert.ToString(oComplementoFesp.RetencionIva);

            XmlNode NTotalMenosRetenciones = DocXML.CreateElement("cfe", "TotalMenosRetenciones", cfe);  //NODO Total
            NRetencionesFacturaEspecial.AppendChild(NTotalMenosRetenciones);
            NTotalMenosRetenciones.InnerText = Convert.ToString(oComplementoFesp.TotalMenosRetenciones);

            return DocXML;

        }
    }
}
