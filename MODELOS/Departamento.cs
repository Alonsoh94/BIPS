using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Departamento
    {
        public Departamento()
        {
            Municipios = new HashSet<Municipio>();
        }

        public int Id { get; set; }
        public string NombreDepartamento { get; set; } = null!;
        public int Pais { get; set; }

        public virtual Paise PaisNavigation { get; set; } = null!;
        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
