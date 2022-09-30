using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Moneda
    {
        public Moneda()
        {
            Empresas = new HashSet<Empresa>();
        }

        public int Id { get; set; }
        public string NombreMoneda { get; set; } = null!;
        public string AcronimoMoneda { get; set; } = null!;

        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
