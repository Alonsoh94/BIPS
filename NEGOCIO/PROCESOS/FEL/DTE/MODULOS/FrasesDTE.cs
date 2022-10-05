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
    public class FrasesDTE
    {
        XmlNode DatosEmision;
        PedidoPv oPedido;
        TipoDocumentoFiscal oDoctoFiscal;        
        BIPSContext dbContext;


        public XmlDocument ModuloFrasesDTE(XmlDocument DocXML, string dte, long Id)
        {
            List<FrasesEscenariosFiscale> FrasesDTEList = new List<FrasesEscenariosFiscale>();
            NodosInterface node = new DatosGeneralesDTE();
            NodosInterface nodoEstructura = new EstructuraDTE();
            oPedido = node.PedidoActual();
            DatosEmision = nodoEstructura.NodoDatosEmision();

            using(dbContext = new BIPSContext())
            {
                oDoctoFiscal = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault<TipoDocumentoFiscal>();
               FrasesDTEList  = dbContext.FrasesEscenariosFiscales.Where(f => f.TipoDocumentoFiscal == oDoctoFiscal.Id).ToList<FrasesEscenariosFiscale>();
            }
            if(FrasesDTEList.Count > 0)
            {
                try
                {
                    XmlNode NFrases = DocXML.CreateElement("dte", "Frases", dte);
                    DatosEmision.AppendChild(NFrases);

                    foreach (var item in FrasesDTEList)
                    {

                        XmlNode NFrase = DocXML.CreateElement("dte", "Frase", dte);
                        NFrases.AppendChild(NFrase);

                        XmlAttribute ACodigoEscenario = DocXML.CreateAttribute("CodigoEscenario");
                        ACodigoEscenario.Value = Convert.ToString(item.Escenario);
                        NFrase.Attributes.Append(ACodigoEscenario);

                        XmlAttribute ATipoFrase = DocXML.CreateAttribute("TipoFrase");
                        ATipoFrase.Value = Convert.ToString(item.Frase);
                        NFrase.Attributes.Append(ATipoFrase);

                    }

                }
                catch (Exception)
                {

                    throw;
                }

            }
            


            return DocXML;
        }
    }
}
