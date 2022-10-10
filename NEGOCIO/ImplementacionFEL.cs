using BIPS.NEGOCIO.PROCESOS.FEL.DTE.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO
{
    public class ImplementacionFEL
    {
        XMLCertificacion XMLCertificacion = new();
        XMLAnulacion oXMLAnulacion = new XMLAnulacion();
        public void ProbarImplementacion()
        {
            int digito = 1;
            XMLCertificacion.GenerarXMLCertificacion(digito);
        }

        public void ProbarAnulacion()
        {
            int digitoA = 22;
            oXMLAnulacion.GenerarXMLAnulacion(digitoA);
        }
    }
}
