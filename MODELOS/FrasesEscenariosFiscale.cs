using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FrasesEscenariosFiscale
    {
        public int Id { get; set; }
        public int Frase { get; set; }
        public int Escenario { get; set; }
        public string? NumeroResolucion { get; set; }
        public DateTime? FechaResolucion { get; set; }
        public int? TipoDocumentoFiscal { get; set; }

        public virtual TipoDocumentoFiscal? TipoDocumentoFiscalNavigation { get; set; }
    }
}
