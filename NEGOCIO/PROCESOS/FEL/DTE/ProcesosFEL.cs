using BIPS.NEGOCIO.PROCESOS.FACTURACION.IMPLEMENTACION;
using BIPS.NEGOCIO.PROCESOS.FEL.DTE.GENERADORXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BIPS.NEGOCIO.PROCESOS.FEL.DTE
{
    public class ProcesosFEL 
    {
        public static string Mensaje = string.Empty;
        public async Task<bool> Anulacion()
        {
            //Mensaje = string.Empty;
            XMLAnulacion xMLAnulacion = new XMLAnulacion();
            bool Respuesta = await xMLAnulacion.AnularDocumento(28);
            if (Respuesta == false)
            {
                Mensaje = xMLAnulacion.MensajeResultado();
            }
            return Respuesta;
        }
        
        public async Task<bool> Certificacion()
        {
            bool result = true;
           
            bool ResultFirma;
            bool ResultadoCertificacion = false;
            bool ResultadoImpresion = false;
            XMLCertificacion xMLCertificacion = new XMLCertificacion();
            XmlDocument DocumentoXML = await xMLCertificacion.GenerarXMLCertificacion(1);
            if(DocumentoXML != null )
            {
                ResultFirma = await  xMLCertificacion.FirmarDocumento();
                if(ResultFirma == true)
                {
                   ResultadoCertificacion = await xMLCertificacion.CertificarDocumento();
                    if(ResultadoCertificacion == true)
                    {
                        ResultadoImpresion = await xMLCertificacion.ImprimirFactura();
                        result = true;                                               
                    }
                    else
                    {
                        result = false;
                        Mensaje = xMLCertificacion.MensajeCertificacion();
                    }
                }
                else
                {                    
                    result = false;
                    Mensaje = xMLCertificacion.MensajeCertificacion();
                }
            }
            else
            {
                result = false;
                Mensaje = "El XML no se ha construido y ha dado un resultado nulo.";
            }

            if (ResultadoImpresion == false)
            {
                Mensaje = xMLCertificacion.MensajeCertificacion();
            }

            if (ResultadoCertificacion == true)
            {               
                xMLCertificacion.Facturar();

            }

            return result;
        }


        public string MensajeResultado() => Mensaje;
        
    }
}
