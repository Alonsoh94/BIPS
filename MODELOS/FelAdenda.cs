using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FelAdenda
    {
        public int Id { get; set; }
        public string Clave { get; set; } = null!;
        public string Valor { get; set; } = null!;
    }
}
