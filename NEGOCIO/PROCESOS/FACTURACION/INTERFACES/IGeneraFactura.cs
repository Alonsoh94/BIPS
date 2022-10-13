using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FACTURACION.INTERFACES
{
    public interface IGeneraFactura
    {
        public Task<bool> GenerarFactura();
        public string MensajeError();

    }
}
