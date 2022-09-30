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
        public string NombreDocumento { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Nomenclatura { get; set; } = null!;

        public virtual ICollection<FrasesEscenariosFiscale> FrasesEscenariosFiscales { get; set; }
        public virtual ICollection<PedidoPv> PedidoPvs { get; set; }
    }
}
