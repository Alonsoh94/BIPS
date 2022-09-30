using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.MODULOS
{
    public class DatosGeneralesDTE 
    {
        BIPSContext dbContext;

        NodosInterface Nodos = new EstructuraDTE();
        public DatosGeneralesDTE(BIPSContext Contexto)
        {            
            dbContext = Contexto;
        }
       public XmlDocument ModuloDatosGenerales(XmlDocument Documento, string dte)
       {
            
            try
            {
                XmlNode DatosEmision = Nodos.NodoDatosEmision();
                int miId = 2;
                PedidoPv oPedido = dbContext.PedidoPvs.Where(p => p.Id == miId).FirstOrDefault<PedidoPv>();
                Establecimiento oEstablecimiento = dbContext.Establecimientos.Where(e => e.Id == oPedido.Establecimiento).FirstOrDefault<Establecimiento>();
                Empresa oEmpresa = dbContext.Empresas.Where(e => e.Id == oEstablecimiento.Empresa).FirstOrDefault<Empresa>();
                Monedum oMoneda = dbContext.Moneda.Where(m => m.Id == oEmpresa.MonedaBase).FirstOrDefault<Monedum>();
                TipoDocumentoFiscal oTipoDoc = dbContext.TipoDocumentoFiscals.Where(d => d.Id == oPedido.TipoDocumentoFiscal).FirstOrDefault<TipoDocumentoFiscal>();


                // ****-----
                XmlNode NDatosGenerales = Documento.CreateElement("dte", "DatosGenerales", dte);
                DatosEmision.AppendChild(NDatosGenerales);

                XmlAttribute NumeroAcceso = Documento.CreateAttribute("NumeroAcceso");
                NumeroAcceso.Value = Convert.ToString(oPedido.NumeroAcceso).Trim();

                XmlAttribute CodigoMoneda = Documento.CreateAttribute("CodigoMoneda");
                CodigoMoneda.Value = oMoneda.Acronimo.Trim();
                NDatosGenerales.Attributes.Append(CodigoMoneda);


                if (oPedido.LocalOexportacion == false)
                {
                    XmlAttribute AExportacion = Documento.CreateAttribute("Exp");
                    AExportacion.Value = "SI";
                    NDatosGenerales.Attributes.Append(AExportacion);
                }

                //TimeZoneInfo hwZone = TimeZoneInfo.FindSystemTimeZoneById("América/Guatemala");
                //est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                //DateTime FechaHora = oGenerales.FechaHoraEmision; //.ToString("yyyy-MM-ddTHH:mm:ss.fffzz:ff");
                //** //DateTime FechaHora = oGenerales.FechaHoraEmision.ToString("yyyy-MM-ddTHH:mm:ss.fffzz:ff");
                //** string FechaHora = oDatosGenerales.FechaHoraEmision.ToString("yyyy-MM-dd");
                //** string Zonahoraria = "T00:00:00.000-06:00";
                //** string FechaHOraCompleta = FechaHora + Zonahoraria;
                //TimeZoneInfo.ConvertTimeBySystemTimeZoneId(FechaHora, hwZone.ToString());
                string CentroAmerica = "CST";

                TimeZoneInfo ZonaHorariaGuatemala = TimeZoneInfo.FindSystemTimeZoneById(CentroAmerica);
                DateTime NewFecha = TimeZoneInfo.ConvertTimeToUtc(oPedido.Fecha, ZonaHorariaGuatemala);                
                XmlAttribute FechaHoraEmision = Documento.CreateAttribute("FechaHoraEmision");
                FechaHoraEmision.Value = Convert.ToString(NewFecha);
                NDatosGenerales.Attributes.Append(FechaHoraEmision);

                XmlAttribute Tipo = Documento.CreateAttribute("Tipo");
                Tipo.Value = oTipoDoc.Nomenclatura.Trim();
                NDatosGenerales.Attributes.Append(Tipo);
                //----*****

            }
            catch (Exception)
            {

            }



            return Documento;
       }
        


    }
}
