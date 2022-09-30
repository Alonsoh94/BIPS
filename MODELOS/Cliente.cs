using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Nit { get; set; }
        public string? CorreoElectronico { get; set; }
        public int Pais { get; set; }
        public int Departamento { get; set; }
        public int Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? CodigoPostal { get; set; }
        public bool? TipoEspecial { get; set; }

        public virtual Municipio CiudadNavigation { get; set; } = null!;
        public virtual Departamento DepartamentoNavigation { get; set; } = null!;
        public virtual Paise PaisNavigation { get; set; } = null!;
    }
}
