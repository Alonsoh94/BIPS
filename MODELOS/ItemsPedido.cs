using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ItemsPedido
    {
        public long Id { get; set; }
        public long Pedido { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Precio { get; set; }
        public decimal? Descuento { get; set; }
        public string? Descripcion { get; set; }
        public string? UnidadMedida { get; set; }
        public string BienOservicio { get; set; } = null!;

        public virtual Pedido PedidoNavigation { get; set; } = null!;
    }
}
