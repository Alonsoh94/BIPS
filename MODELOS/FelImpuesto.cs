using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FelImpuesto
    {
        public int Id { get; set; }
        public long? Pedido { get; set; }
        public string NombreCorto { get; set; } = null!;
        public decimal TotalMontoImpuesto { get; set; }

        public virtual Pedido? PedidoNavigation { get; set; }
    }
}
