using BIPS.NEGOCIO.PROCESOS.FEL.DTE;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLAnulacion = BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML.XMLAnulacion;
using XMLCertificacion = BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML.XMLCertificacion;

namespace BIPS.NEGOCIO.PROCESOS.FEL
{
    public class ImplementacionFEL
    {
        public static string Mensaje;
        XMLCertificacion XMLCertificacion = new();
        XMLAnulacion oXMLAnulacion = new XMLAnulacion();
        public void ProbarImplementacion()
        {
            int digito = 1;
           //*** XMLCertificacion.GenerarXMLCertificacion(digito);
        }

        public void ProbarAnulacion()
        {
            int digitoA = 22;
          //  oXMLAnulacion.GenerarXMLAnulacion(digitoA);
        }

        public async Task<bool> CertificarDTE()
        {
            ProcesosFEL Proc = new();
            
            bool Resultado = false;
            
            Resultado = await Proc.Certificacion();
            if (Resultado =false)
            {
                Mensaje = Proc.MensajeResultado();
            }
            return Resultado;


        }
        public async Task<bool> AnularDTE()
        {
            bool Resultado = false;
            ProcesosFEL Proc = new();
            Resultado = await Proc.Anulacion();
            if (Resultado == false)
            {
                Mensaje = Proc.MensajeResultado();
            }
            return Resultado;

        }

        public string MensajeResultado() => Mensaje;

        
    }
}
