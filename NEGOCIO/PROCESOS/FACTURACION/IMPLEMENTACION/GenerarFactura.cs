using BIPS.MODELOS;
using BIPS.NEGOCIO.PROCESOS.FACTURACION.INTERFACES;
using BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FACTURACION.IMPLEMENTACION
{
    public class GenerarFactura 
    {
        public static string? Mensaje;
        public string MensajeResultado() => Mensaje;        

        public async Task<bool> GenerarcionFactura(PedidoPv oPedido, Establecimiento oEstablecimiento, Cliente oCliente, ResponseOK Certificado)
        {
            bool Resultado = false;
            CertificarINFILE oCertificarINFILE = new();
            //ResponseOK Certificado = oCertificarINFILE.MiCertificacion();
            decimal calculo = (oPedido.TotalPedido / 1.12m);
            double ValorIva = 10.71;
            try
            {
                using (BIPSContext dbContext = new BIPSContext())
                {
                    Factura Fac = new Factura();
                    Fac.ReferenciaInterna = oPedido.ReferenciaInterna.Trim();
                    Fac.Establecimiento = oEstablecimiento.Id;
                    Fac.TipoCargoCxc = 1;
                    Fac.Cliente = oCliente.Id;
                    Fac.Nit = oCliente.Nit.Trim();
                    Fac.Vendedor = 1;
                    Fac.PedidoPv = oPedido.Id;
                    Fac.FechaFacturacion = DateTime.Now;
                    Fac.Moneda = 1;
                    Fac.TipoCambio = 7.90m;
                    Fac.ValorTotal = oPedido.TotalPedido;
                    Fac.ValorIva = Convert.ToDecimal(ValorIva);
                    Fac.Certificado = true;
                    Fac.NumeroAutorizacionC = Certificado.uuid.Trim();
                    Fac.SerieC = Certificado.serie.Trim();
                    Fac.NumeroDoctoC = Certificado.numero.Trim();
                    Fac.FechaCertificado = DateTime.Now;
                    Fac.EstadoGeneral = true;

                    dbContext.Facturas.Add(Fac);
                    dbContext.SaveChanges();
                    Resultado = true;
                }
            }
            catch (Exception)
            {
                Resultado = false;
                Mensaje = "Error al Generar la Factura en el sistema";
            }
            return Resultado;
        }
    }
}
