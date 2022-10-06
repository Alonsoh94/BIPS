using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
            PedidoPvs = new HashSet<PedidoPv>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Nit { get; set; } = null!;
        public string? CorreoElectronico { get; set; }
        public int Municipio { get; set; }
        public string Direccion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public bool TipoEspecial { get; set; }
        public int Empresa { get; set; }

        public virtual Empresa EmpresaNavigation { get; set; } = null!;
        public virtual Municipio MunicipioNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<PedidoPv> PedidoPvs { get; set; }
    }
}
