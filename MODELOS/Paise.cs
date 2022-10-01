using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Paise
    {
        public Paise()
        {
            Departamentos = new HashSet<Departamento>();
        }

        public int Id { get; set; }
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Acronimo { get; set; } = null!;

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
