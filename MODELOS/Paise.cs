using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Paise
    {
        public Paise()
        {
            Clientes = new HashSet<Cliente>();
            Departamentos = new HashSet<Departamento>();
            Empresas = new HashSet<Empresa>();
        }

        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string? AcronimoPais { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Departamento> Departamentos { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
