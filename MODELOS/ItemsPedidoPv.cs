using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ItemsPedidoPv
    {
        public ItemsPedidoPv()
        {
            ItemsImpuestos = new HashSet<ItemsImpuesto>();
        }

        public long Id { get; set; }
        public long PedidoPv { get; set; }
        public DateTime Fecha { get; set; }
        public int Catidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public decimal Total { get; set; }
        public string? Descripcion { get; set; }
        public string UnidadMedid { get; set; } = null!;
        public string BienOservicio { get; set; } = null!;

        public virtual PedidoPv PedidoPvNavigation { get; set; } = null!;
        public virtual ICollection<ItemsImpuesto> ItemsImpuestos { get; set; }
    }
}
