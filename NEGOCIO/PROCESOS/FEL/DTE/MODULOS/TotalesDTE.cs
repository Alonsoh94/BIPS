using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class TotalesDTE
    {    
        ImpuestosPedido oImpuestosPedido;
        XmlNode DatosEmision;       
        BIPSContext dbContext;

        public XmlDocument ModuloTotales(XmlDocument DocXML , TipoDocumentoFiscal oTipoDocumentoFiscal, PedidoPv oPedido, string dte, long Id)
        {
            List<ImpuestosPedido> ListaImpuestosPedidos = new List<ImpuestosPedido>();

            NodosInterface nodoEstructura = new EstructuraDTE();
            DatosEmision = nodoEstructura.NodoDatosEmision();
            
            try
            {
               using (dbContext = new BIPSContext())
                {
                    ListaImpuestosPedidos = dbContext.ImpuestosPedidos.Where(i => i.PedidoPv == oPedido.Id).ToList();
                }
            }
            catch (Exception)
            {

                throw;  //*****************************
            }

            try
            {
                XmlNode Totales = DocXML.CreateElement("dte", "Totales", dte);
                DatosEmision.AppendChild(Totales);

                if (ListaImpuestosPedidos.Count > 0)
                {                    
                    if (oTipoDocumentoFiscal.Nomenclatura != "NABN")
                    {
                        foreach (var item in ListaImpuestosPedidos)
                        {
                            XmlNode NTotalImpuestos = DocXML.CreateElement("dte", "TotalImpuestos", dte);  // nodo Totales impusto
                            Totales.AppendChild(NTotalImpuestos);

                            XmlNode NTotalImpuesto = DocXML.CreateElement("dte", "TotalImpuesto", dte);  // nodo Totales impusto
                            NTotalImpuestos.AppendChild(NTotalImpuesto);

                            XmlAttribute ANombreCorto = DocXML.CreateAttribute("NombreCorto");
                            ANombreCorto.Value = Convert.ToString(item.NombreCorto); //Definir
                            NTotalImpuesto.Attributes.Append(ANombreCorto);

                            XmlAttribute ATotalMontoImpuesto = DocXML.CreateAttribute("TotalMontoImpuesto");
                            ATotalMontoImpuesto.Value = Convert.ToString(item.TotalMontoImpuesto); //Definir
                            NTotalImpuesto.Attributes.Append(ATotalMontoImpuesto);

                        }
                    }
                }

                XmlNode GranTotal = DocXML.CreateElement("dte", "GranTotal", dte);  // nodo Gran Total
                Totales.AppendChild(GranTotal);
                GranTotal.InnerText = Convert.ToString(oPedido.TotalPedido);

            }
            catch (Exception)
            {

                throw;  ///***********************************
            }

            return DocXML;
        }
    }
}
