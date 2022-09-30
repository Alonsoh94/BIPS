using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class PedidoPv
    {
        public PedidoPv()
        {
            ImpuestosPedidos = new HashSet<ImpuestosPedido>();
            ItemsPedidoPvs = new HashSet<ItemsPedidoPv>();
        }

        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cliente { get; set; }
        public bool? Estado { get; set; }
        public int Establecimiento { get; set; }
        public int Usuario { get; set; }
        public bool? AplicaIva { get; set; }
        public decimal MontoPedido { get; set; }
        public decimal? PorcentajeDescuento { get; set; }
        public decimal? MontoDescuento { get; set; }
        public decimal TotalPedido { get; set; }
        public int? Items { get; set; }
        public string? Referencia { get; set; }
        public int TipoDocumentoFiscal { get; set; }
        public long NumeroAcceso { get; set; }
        public bool? LocalOexportacion { get; set; }

        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual Establecimiento EstablecimientoNavigation { get; set; } = null!;
        public virtual TipoDocumentoFiscal TipoDocumentoFiscalNavigation { get; set; } = null!;
        public virtual ICollection<ImpuestosPedido> ImpuestosPedidos { get; set; }
        public virtual ICollection<ItemsPedidoPv> ItemsPedidoPvs { get; set; }
    }
}
