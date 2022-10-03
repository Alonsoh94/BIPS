using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ComplementoFcam
    {
        public int Id { get; set; }
        public long PedidoPv { get; set; }
        public int Idcomplemento { get; set; }
        public string NombreComplemento { get; set; } = null!;
        public string Uricomplemento { get; set; } = null!;
        public decimal Version { get; set; }
        public int NumeroAbono { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoAbono { get; set; }

        public virtual PedidoPv PedidoPvNavigation { get; set; } = null!;
    }
}
