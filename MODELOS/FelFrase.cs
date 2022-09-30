using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FelFrase
    {
        public int Id { get; set; }
        public int Frase { get; set; }
        public int Escenario { get; set; }
        public string? NumeroResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public int TipoDocumento { get; set; }

        public virtual TipoDocumento TipoDocumentoNavigation { get; set; } = null!;
    }
}
