using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Empresa
    {
        public Empresa()
        {
            Adenda = new HashSet<Adendum>();
            Clientes = new HashSet<Cliente>();
            ConfiguracionesFels = new HashSet<ConfiguracionesFel>();
            Establecimientos = new HashSet<Establecimiento>();
        }

        public int Id { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string NombreComercial { get; set; } = null!;
        public string Rtu { get; set; } = null!;
        public string? CorreoElectronico { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public int MonedaBase { get; set; }
        public string? NumeroPatronal { get; set; }
        public string? RepresentanteLegal { get; set; }
        public string? RegimenIva { get; set; }
        public int Municipio { get; set; }
        public string Direccion { get; set; } = null!;
        public bool? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public virtual Monedum MonedaBaseNavigation { get; set; } = null!;
        public virtual Municipio MunicipioNavigation { get; set; } = null!;
        public virtual ICollection<Adendum> Adenda { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<ConfiguracionesFel> ConfiguracionesFels { get; set; }
        public virtual ICollection<Establecimiento> Establecimientos { get; set; }
    }
}
