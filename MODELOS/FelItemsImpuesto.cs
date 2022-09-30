using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FelItemsImpuesto
    {
        public long Id { get; set; }
        public long Item { get; set; }
        public string NombreCorto { get; set; } = null!;
        public int CodigoUnidadGravable { get; set; }
        public decimal MontoGravable { get; set; }
        public int CantidadUnidadGravables { get; set; }
        public decimal? MontoImpuesto { get; set; }
    }
}
