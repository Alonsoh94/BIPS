using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Municipio
    {
        public Municipio()
        {
            Clientes = new HashSet<Cliente>();
            Empresas = new HashSet<Empresa>();
        }

        public int Id { get; set; }
        public string NombreMunicipio { get; set; } = null!;
        public int Departamento { get; set; }

        public virtual Departamento DepartamentoNavigation { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
