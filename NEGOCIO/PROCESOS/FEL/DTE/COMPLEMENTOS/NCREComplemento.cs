using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.COMPLEMENTOS
{
    public class NCREComplemento
    {
        PedidoPv oPedido;
        XmlNode DatosEmision;
        BIPSContext dbContext;

        public XmlDocument ComplementoNCRE(XmlDocument DocXML, string dte, long Id, string cno, string xsi)
        {
            NodosInterface nodos = new EstructuraDTE();
            NodosInterface DatosGenerales = new DatosGeneralesDTE();
            DatosEmision = nodos.NodoDatosEmision();
            oPedido = DatosGenerales.PedidoActual();
            ComplementoNota ComNotas = new();
            using (dbContext = new BIPSContext())
            {
                if(dbContext.ComplementoNotas.Any(e => e.PedidoPv == Id))
                {
                    ComNotas = dbContext.ComplementoNotas.Where(e => e.PedidoPv == Id).FirstOrDefault();

                    
                }
            }

            XmlNode NComplementos = DocXML.CreateElement("dte", "Complementos", dte);
            DatosEmision.AppendChild(NComplementos);

            XmlNode NodoComplemento = DocXML.CreateElement("dte", "Complemento", dte);
            NComplementos.AppendChild(NodoComplemento);

            XmlAttribute AIDComplemento = DocXML.CreateAttribute("IDComplemento");
            AIDComplemento.Value = Convert.ToString(ComNotas.Idcomplemento);
            NodoComplemento.Attributes.Append(AIDComplemento);

            XmlAttribute ANombreComplemento = DocXML.CreateAttribute("NombreComplemento");
            ANombreComplemento.Value = Convert.ToString(ComNotas.NombreComplemento);
            NodoComplemento.Attributes.Append(ANombreComplemento);

            XmlAttribute URIComplemento = DocXML.CreateAttribute("URIComplemento");
            URIComplemento.Value = Convert.ToString(ComNotas.Uricomplemento.Trim());
            NodoComplemento.Attributes.Append(URIComplemento);

            //NODO REFERENCIA NOTA
            XmlNode NReferenciasNota = DocXML.CreateElement("cno", "ReferenciasNota", cno);  // nodo Gran Total
            NodoComplemento.AppendChild(NReferenciasNota);

            XmlAttribute Axmlnscno = DocXML.CreateAttribute("xmlns:cno");
            Axmlnscno.Value = cno;
            NReferenciasNota.Attributes.Append(Axmlnscno);

            XmlAttribute AFechaEmisionOrigen = DocXML.CreateAttribute("FechaEmisionDocumentoOrigen");
            AFechaEmisionOrigen.Value = Convert.ToDateTime(ComNotas.FechaEmisionDocumentoOrigen).ToString("yyyy-MM-dd");
            NReferenciasNota.Attributes.Append(AFechaEmisionOrigen);

            XmlAttribute AMotivoAjuste = DocXML.CreateAttribute("MotivoAjuste");
            AMotivoAjuste.Value = Convert.ToString(ComNotas.MotivoAjuste.Trim());
            NReferenciasNota.Attributes.Append(AMotivoAjuste);

            XmlAttribute ANumeroAutorizacionDocumentoOrigen = DocXML.CreateAttribute("NumeroAutorizacionDocumentoOrigen");
            ANumeroAutorizacionDocumentoOrigen.Value = Convert.ToString(ComNotas.NumeroAutorizacionDocumentoOrigen.Trim());
            NReferenciasNota.Attributes.Append(ANumeroAutorizacionDocumentoOrigen);

            // XmlAttribute ANumeroDocumentoOrigen = oXML.CreateAttribute("NumeroDocumentoOrigen");
            // ANumeroDocumentoOrigen.Value = Convert.ToString(item[10]).Trim();
            // NReferenciasNota.Attributes.Append(ANumeroDocumentoOrigen);

            XmlAttribute AVersion = DocXML.CreateAttribute("Version");
            AVersion.Value = Convert.ToString(ComNotas.Version);
            NReferenciasNota.Attributes.Append(AVersion);

            if ("********" == "INFILE")
            {
                XmlAttribute AxsischemaLocation = DocXML.CreateAttribute("xsi", "schemaLocation", xsi);
                AxsischemaLocation.Value = @"http://www.sat.gob.gt/face2/ComplementoReferenciaNota/0.1.0 C:\Users\User\Desktop\FEL\Esquemas\GT_Complemento_Referencia_Nota-0.1.0.xsd";
                NReferenciasNota.Attributes.Append(AxsischemaLocation);
            }
            return DocXML;
        }
    }
}
