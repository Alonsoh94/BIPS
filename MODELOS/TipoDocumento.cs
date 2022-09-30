using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            FelFrases = new HashSet<FelFrase>();
        }

        public int Id { get; set; }
        public string NombreDocumento { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Nomenglatura { get; set; } = null!;

        public virtual ICollection<FelFrase> FelFrases { get; set; }
    }
}
