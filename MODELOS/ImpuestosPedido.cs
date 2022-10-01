using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ImpuestosPedido
    {
        public long Id { get; set; }
        public long PedidoPv { get; set; }
        public string NombreCorto { get; set; } = null!;
        public decimal TotalMontoImpuesto { get; set; }

        public virtual PedidoPv PedidoPvNavigation { get; set; } = null!;
    }
}
