using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class PedidoPv
    {
        public PedidoPv()
        {
            ComplementoExpos = new HashSet<ComplementoExpo>();
            ComplementoFcams = new HashSet<ComplementoFcam>();
            ComplementoFesps = new HashSet<ComplementoFesp>();
            ImpuestosPedidos = new HashSet<ImpuestosPedido>();
            ItemsPedidoPvs = new HashSet<ItemsPedidoPv>();
        }

        public long Id { get; set; }
        public string? ReferenciaInterna { get; set; }
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
        public int TipoDocumentoFiscal { get; set; }
        public long NumeroAcceso { get; set; }
        public bool? LocalOexportacion { get; set; }

        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual Establecimiento EstablecimientoNavigation { get; set; } = null!;
        public virtual TipoDocumentoFiscal TipoDocumentoFiscalNavigation { get; set; } = null!;
        public virtual ICollection<ComplementoExpo> ComplementoExpos { get; set; }
        public virtual ICollection<ComplementoFcam> ComplementoFcams { get; set; }
        public virtual ICollection<ComplementoFesp> ComplementoFesps { get; set; }
        public virtual ICollection<ImpuestosPedido> ImpuestosPedidos { get; set; }
        public virtual ICollection<ItemsPedidoPv> ItemsPedidoPvs { get; set; }
    }
}
