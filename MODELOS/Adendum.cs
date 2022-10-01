using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Adendum
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public string Valor { get; set; } = null!;
        public int Empresa { get; set; }

        public virtual Empresa EmpresaNavigation { get; set; } = null!;
    }
}
