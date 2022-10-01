using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class TipoDocumentoFiscal
    {
        public TipoDocumentoFiscal()
        {
            FrasesEscenariosFiscales = new HashSet<FrasesEscenariosFiscale>();
            PedidoPvs = new HashSet<PedidoPv>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Nomenclatura { get; set; } = null!;

        public virtual ICollection<FrasesEscenariosFiscale> FrasesEscenariosFiscales { get; set; }
        public virtual ICollection<PedidoPv> PedidoPvs { get; set; }
    }
}
