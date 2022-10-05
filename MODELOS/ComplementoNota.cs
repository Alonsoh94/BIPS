using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ComplementoNota
    {
        public int Id { get; set; }
        public long PedidoPv { get; set; }
        public int? Idcomplemento { get; set; }
        public string? NombreComplemento { get; set; }
        public string Uricomplemento { get; set; } = null!;
        public DateTime? FechaEmisionDocumentoOrigen { get; set; }
        public string? MotivoAjuste { get; set; }
        public string? NumeroAutorizacionDocumentoOrigen { get; set; }
        public string? Version { get; set; }
    }
}
