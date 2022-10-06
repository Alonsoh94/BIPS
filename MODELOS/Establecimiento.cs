using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Establecimiento
    {
        public Establecimiento()
        {
            Facturas = new HashSet<Factura>();
            PedidoPvs = new HashSet<PedidoPv>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public int Municipio { get; set; }
        public int Empresa { get; set; }

        public virtual Empresa EmpresaNavigation { get; set; } = null!;
        public virtual Municipio MunicipioNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<PedidoPv> PedidoPvs { get; set; }
    }
}
