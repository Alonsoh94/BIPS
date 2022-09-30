using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Pedido
    {
        public Pedido()
        {
            FelImpuestos = new HashSet<FelImpuesto>();
            ItemsPedidos = new HashSet<ItemsPedido>();
        }

        public long Id { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Cliente { get; set; }
        public bool? Estado { get; set; }
        public int? Empresa { get; set; }
        public int? Usuario { get; set; }
        public bool? AplicaIva { get; set; }
        public decimal MontoPedido { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public decimal MontoDescuento { get; set; }
        public decimal? MontoFactura { get; set; }
        public int? Items { get; set; }
        public string? Referencia { get; set; }
        public string TipoDocumento { get; set; } = null!;
        public long NumeroAcceso { get; set; }
        public bool? LocalOexportacion { get; set; }
        public byte[]? TipoPersoneria { get; set; }
        public int Establecimiento { get; set; }

        public virtual ICollection<FelImpuesto> FelImpuestos { get; set; }
        public virtual ICollection<ItemsPedido> ItemsPedidos { get; set; }
    }
}
