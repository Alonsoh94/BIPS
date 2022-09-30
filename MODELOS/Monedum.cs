using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BIPS.MODELOS
{
    public partial class Monedum
    {      
        public Monedum()
        {
            Empresas = new HashSet<Empresa>();
        }

        public int Id { get; set; }
        public string? NombreMoneda { get; set; }
        public string? Acronimo { get; set; }

        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
