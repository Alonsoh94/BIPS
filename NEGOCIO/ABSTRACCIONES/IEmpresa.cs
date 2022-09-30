using BIPS.MODELOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.ABSTRACCIONES
{
    public interface IEmpresa
    {
        public void Crear(Empresa empresa);
        public void Editar(Empresa empresa);
        public void Eliminar(int Id);  
        public Empresa GetEmpresa(int Id);
        public List<Empresa> ListaEmpresas();
    }
}
