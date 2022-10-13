using BIPS.MODELOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML
{
    public interface NodosInterface
    {
        public XmlNode NodoDatosEmision();

        public PedidoPv PedidoActual();
        public XmlNode NodoSAT();
    }
}
