using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Departamento
    {
        public Departamento()
        {
            Clientes = new HashSet<Cliente>();
            Empresas = new HashSet<Empresa>();
            Municipios = new HashSet<Municipio>();
        }

        public int Id { get; set; }
        public string StateName { get; set; } = null!;
        public int? Country { get; set; }

        public virtual Paise? CountryNavigation { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
