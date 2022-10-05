using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class ResponseOK
    {
        public string resultado { get; set; }
        public string descripcion { get; set; }
        public string archivo { get; set; }

        // Certificacion
        public int cantidad_errores { get; set; }
        public List<Errores> descripcion_errores;
        public string uuid { get; set; }
        public string serie { get; set; }
        public string numero { get; set; }
        public string xml_certificado { get; set; }



    }

    public class Errores
    {
        public string resutado { get; set; }
        public string fuente { get; set; }
        public string categoria { get; set; }
        public string numeral { get; set; }
        public string validacion { get; set; }
        public string mensaje_error { get; set; }


    }

}
