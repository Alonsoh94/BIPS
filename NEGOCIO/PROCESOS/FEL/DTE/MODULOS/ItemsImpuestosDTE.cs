using BIPS.MODELOS;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class ItemsImpuestosDTE
    {
        static List<ItemsImpuesto> ListaItemsImpuestos = new List<ItemsImpuesto>();
        BIPSContext dbContext;
        public XmlDocument ModuloItemsImpuestosDTE(XmlDocument DocXML, string dte, long IdItem,XmlNode Item)
        {
            
            try
            {
                using(dbContext = new BIPSContext())
                {
                    ListaItemsImpuestos = dbContext.ItemsImpuestos.Where(i => i.ItemsPedidoPv == IdItem).ToList();
                }

            }
            catch (Exception)
            {

                throw;
            }

            try
            {
                if(ListaItemsImpuestos.Count > 0)
                {
                    XmlNode Impuestos = DocXML.CreateElement("dte", "Impuestos", dte);
                    Item.AppendChild(Impuestos);

                    foreach (var item in ListaItemsImpuestos)
                    {
                        XmlNode NImpuesto = DocXML.CreateElement("dte", "Impuesto", dte);
                        Impuestos.AppendChild(NImpuesto);

                        XmlNode NNombreCorto = DocXML.CreateElement("dte", "NombreCorto", dte);
                        NImpuesto.AppendChild(NNombreCorto);
                        NNombreCorto.InnerText = Convert.ToString(item.NombreCorto.Trim());

                        XmlNode NCodigoUnidadGravable = DocXML.CreateElement("dte", "CodigoUnidadGravable", dte);
                        NImpuesto.AppendChild(NCodigoUnidadGravable);
                        NCodigoUnidadGravable.InnerText = Convert.ToString(item.CodigoUnidadGravable.Trim());

                        XmlNode NMontoGravable = DocXML.CreateElement("dte", "MontoGravable", dte);
                        NImpuesto.AppendChild(NMontoGravable);
                        NMontoGravable.InnerText = Convert.ToString(item.MontoGravable);

                        XmlNode NMontoImpuesto = DocXML.CreateElement("dte", "MontoImpuesto", dte);
                        NImpuesto.AppendChild(NMontoImpuesto);
                        NMontoImpuesto.InnerText = Convert.ToString(item.MontoImpuesto);

                    }
                }

            }
            catch (Exception)
            {

                throw;
            }


            return DocXML;
        }
    }
}
