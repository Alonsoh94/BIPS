using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ComplementoExpo
    {
        public int Id { get; set; }
        public long PedidoPv { get; set; }
        public int Idcomplemento { get; set; }
        public string NombreComplemento { get; set; } = null!;
        public string Uricomplemento { get; set; } = null!;
        public string Version { get; set; } = null!;
        public string NombreConsignatarioOdestinatario { get; set; } = null!;
        public string DireccionConsignatarioOdestinatario { get; set; } = null!;
        public string NombreComprador { get; set; } = null!;
        public string DireccionComprador { get; set; } = null!;
        public string Incoterm { get; set; } = null!;
        public string CodigoExportador { get; set; } = null!;
        public string OtraReferencia { get; set; } = null!;
        public string CodigoComprador { get; set; } = null!;
        public string NombreExportador { get; set; } = null!;

        public virtual PedidoPv PedidoPvNavigation { get; set; } = null!;
    }
}
