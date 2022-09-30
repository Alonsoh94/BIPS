using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ImpuestosPedido
    {
        public long Id { get; set; }
        public long? Pedido { get; set; }
        public string NombreCorto { get; set; } = null!;
        public decimal TotalMontoImpuesto { get; set; }

        public virtual PedidoPv? PedidoNavigation { get; set; }
    }
}
