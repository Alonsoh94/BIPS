using BIPS.MODELOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FACTURACION.IMPLEMENTACION
{
    public class RevertirFacturacion
    {
        public async Task<bool> ActualizaFacturaPorAnualacion(long id, string Referencia, string Serie, string NoAutorizacionDocto, string UUID)
        {
            bool resultado = false;
            using (BIPSContext dbContext = new BIPSContext())
            {
                var Fac = dbContext.Facturas.FirstOrDefault(f => f.Id == id);

                Fac.FechaAnulado = DateTime.Now;
                Fac.SerieA = Serie;
                Fac.NumeroAutorizacionA = UUID;
                Fac.NumeroDoctoA = NoAutorizacionDocto;
                Fac.ReferenciaInternaAnulacion = Referencia;
                Fac.EstadoGeneral = false;
                Fac.Anulado = true;

                dbContext.SaveChanges();
            }


            return resultado;

        }
    }
}
