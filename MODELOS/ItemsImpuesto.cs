using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ItemsImpuesto
    {
        public long Id { get; set; }
        public long ItemsPedidoPv { get; set; }
        public string NombreCorto { get; set; } = null!;
        public string CodigoUnidadGravable { get; set; } = null!;
        public decimal MontoGravable { get; set; }
        public decimal MontoImpuesto { get; set; }

        public virtual ItemsPedidoPv ItemsPedidoPvNavigation { get; set; } = null!;
    }
}
