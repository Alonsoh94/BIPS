using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class ItemsDTE
    {
        XmlNode DatosEmision;
        PedidoPv oPedido;
        TipoDocumentoFiscal oDoctoFiscal;
        BIPSContext dbContext;
        public XmlDocument ModuloItemsDTE(XmlDocument DocXML, string dte, int Id)
        {
          List<ItemsPedidoPv> items = new List<ItemsPedidoPv>();
            NodosInterface nodoEstructura = new EstructuraDTE();
            DatosEmision = nodoEstructura.NodoDatosEmision();
            NodosInterface node = new DatosGeneralesDTE();
            oPedido = node.PedidoActual();


            try
            {
                using(dbContext = new BIPSContext())
                {
                    items = dbContext.ItemsPedidoPvs.Where(i => i.PedidoPv == oPedido.Id).ToList<ItemsPedidoPv>();
                    oDoctoFiscal = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault<TipoDocumentoFiscal>();
                }

            }
            catch (Exception)
            {

                throw;
            }
            if(items.Count > 0)
            {
                try
                {
                    XmlNode NItems = DocXML.CreateElement("dte", "Items", dte);  // nodo Items
                    DatosEmision.AppendChild(NItems);

                    int contador = 0;
                    foreach (var item in items)
                    {
                        contador++;

                        XmlNode NItem = DocXML.CreateElement("dte", "Item", dte);  // nodo Item
                        NItems.AppendChild(NItem);

                        XmlAttribute ANumeroLinea = DocXML.CreateAttribute("NumeroLinea");
                        ANumeroLinea.Value = Convert.ToString(contador);
                        NItem.Attributes.Append(ANumeroLinea);
                        
                        XmlAttribute ABienOServicio = DocXML.CreateAttribute("BienOServicio");
                        ABienOServicio.Value = Convert.ToString(item.BienOservicio.Trim());
                        NItem.Attributes.Append(ABienOServicio);

                        XmlNode NCantidad = DocXML.CreateElement("dte", "Cantidad", dte);
                        NItem.AppendChild(NCantidad);
                        NCantidad.InnerText = Convert.ToString(item.Catidad);

                        if (item.UnidadMedid != string.Empty | item.UnidadMedid != null)
                        {
                            XmlNode NUnidadMedida = DocXML.CreateElement("dte", "UnidadMedida", dte);
                            NItem.AppendChild(NUnidadMedida);
                            NUnidadMedida.InnerText = Convert.ToString(item.UnidadMedid.Trim());

                        }

                        XmlNode NDescripcion = DocXML.CreateElement("dte", "Descripcion", dte);
                        NItem.AppendChild(NDescripcion);
                        NDescripcion.InnerText = Convert.ToString(item.Descripcion);

                        XmlNode NPrecioUnitario = DocXML.CreateElement("dte", "PrecioUnitario", dte);
                        NItem.AppendChild(NPrecioUnitario);
                        NPrecioUnitario.InnerText = Convert.ToString(item.PrecioUnitario);

                        XmlNode Precio = DocXML.CreateElement("dte", "Precio", dte);
                        NItem.AppendChild(Precio);
                        Precio.InnerText = Convert.ToString(item.Precio);
                        //decimal Precio = Convert.ToDecimal(MIItem[8]);

                        XmlNode Descuento = DocXML.CreateElement("dte", "Descuento", dte);
                        NItem.AppendChild(Descuento);
                        Descuento.InnerText = Convert.ToString(item.Descuento);
                       // decimal Descuento = Convert.ToDecimal(MIItem[9]);

                      //  decimal TotalItemCalculado = Precio - Descuento;

                        // SECCION DE ITEMS IMPUESTOS
                        if (oDoctoFiscal.Nomenclatura != "NABN")
                        {
                            ItemsImpuestosDTE oItemImpuestos = new ItemsImpuestosDTE();
                            oItemImpuestos.ModuloItemsImpuestosDTE(DocXML, dte, item.Id,NItems);
                        }

                        XmlNode NTotal = DocXML.CreateElement("dte", "Total", dte);
                        NItem.AppendChild(NTotal);
                        NTotal.InnerText = Convert.ToString((item.Precio - item.Descuento));


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
