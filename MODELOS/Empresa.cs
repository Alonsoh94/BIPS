using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Empresa
    {
        public Empresa()
        {
            FelConfiguracions = new HashSet<FelConfiguracion>();
        }

        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string NombreComercial { get; set; } = null!;
        public string? Rtu { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public int MonedaBase { get; set; }
        public string? NumeroPatronal { get; set; }
        public string RepresentanteLegal { get; set; } = null!;
        public string RegimenIva { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public int? Municipio { get; set; }
        public int? Departamento { get; set; }
        public int? Pais { get; set; }
        public bool? Estado { get; set; }

        public virtual Departamento? DepartamentoNavigation { get; set; }
        public virtual Moneda MonedaBaseNavigation { get; set; } = null!;
        public virtual Municipio? MunicipioNavigation { get; set; }
        public virtual Paise? PaisNavigation { get; set; }
        public virtual ICollection<FelConfiguracion> FelConfiguracions { get; set; }
    }
}
