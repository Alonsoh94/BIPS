using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ComplementoFesp
    {
        public int Id { get; set; }
        public long PedidoPv { get; set; }
        public int Idcomplemento { get; set; }
        public string NombreComplemento { get; set; } = null!;
        public string Uricomplemento { get; set; } = null!;
        public decimal RetencionIsr { get; set; }
        public decimal RetencionIva { get; set; }
        public decimal TotalMenosRetenciones { get; set; }
        public string? Version { get; set; }

        public virtual PedidoPv PedidoPvNavigation { get; set; } = null!;
    }
}
