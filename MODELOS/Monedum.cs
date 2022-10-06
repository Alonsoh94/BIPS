using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Monedum
    {
        public Monedum()
        {
            Empresas = new HashSet<Empresa>();
            Facturas = new HashSet<Factura>();
        }

        public int Id { get; set; }
        public string NombreMoneda { get; set; } = null!;
        public string Acronimo { get; set; } = null!;

        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}
