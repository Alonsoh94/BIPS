using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class EmisorDTE
    {
        Establecimiento oEstablecimiento = new();
        PedidoPv oPedido;
        XmlNode DatosEmision;
        BIPSContext dbContext;
        public EmisorDTE(BIPSContext context)
        {
            dbContext = context;
            
        }

        public XmlDocument ModuloEmisorDTE(XmlDocument DocXML, string dte, int Id)
        {
            NodosInterface nodos = new EstructuraDTE();
            NodosInterface DatosGenerales = new DatosGeneralesDTE(dbContext);
            DatosEmision = nodos.NodoDatosEmision();
            oPedido = DatosGenerales.PedidoActual();

            Empresa oEmpresa = new Empresa();
            try
            {
                using (BIPSContext dbContext = new BIPSContext())
                {
                    if (dbContext.Empresas.Any(e => e.Id == Id))
                    {
                        oEmpresa = dbContext.Empresas.Where(e => e.Id == Id).FirstOrDefault<Empresa>();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                using (BIPSContext dbContext = new BIPSContext())
                {
                    if (dbContext.Establecimientos.Any(e => e.Id == oPedido.Establecimiento))
                    {
                        oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oPedido.Establecimiento).FirstOrDefault<Establecimiento>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            
            XmlNode Emisor = DocXML.CreateElement("dte", "Emisor", dte);
            DatosEmision.AppendChild(Emisor);

            XmlAttribute AAfiliacionIVA = DocXML.CreateAttribute("AfiliacionIVA");
            AAfiliacionIVA.Value = oEmpresa.RegimenIva.Trim();
            Emisor.Attributes.Append(AAfiliacionIVA);

            XmlAttribute ACodigoEstablecimiento = DocXML.CreateAttribute("CodigoEstablecimiento");
            ACodigoEstablecimiento.Value = Convert.ToString(oEstablecimiento.Id);
            Emisor.Attributes.Append(ACodigoEstablecimiento);


            return DocXML;
        }
    }
}
