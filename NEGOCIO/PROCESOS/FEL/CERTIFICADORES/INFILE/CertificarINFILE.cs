using BIPS.MODELOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BIPS.NEGOCIO.PROCESOS.FEL.CERTIFICADORES.INFILE
{
    public class CertificarINFILE
    {
        public string? nit_emisor { get; set; }
        public string? correo_copia { get; set; }
        public string? xml_dte { get; set; }

        public static string? ArchivoReq;
        public static bool ResultadoReq;
        public static string? DescripcionReq;
        static ResponseOK RespuestaCertificada = new();
        public async Task<bool> CertificarDocumento(CertificarINFILE ObjCertificar, ConfiguracionesFel Confi, string Referencia)
        {
             
            var ObjCertificarJson = JsonConvert.SerializeObject(ObjCertificar);

          //  string URI = "https://certificador.feel.com.gt/fel/certificacion/v2/dte";

            using (HttpClient hCliente = new HttpClient())
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, Confi.Urlcertificar.Trim()))
                {
                    // requestMessage.Headers.Add("Content-Type", "application/json");
                    requestMessage.Headers.Add("Accept", "application/json");
                    requestMessage.Headers.Add("Method", "POST");
                    requestMessage.Headers.Add("usuario", Confi.Usuario.Trim());
                    requestMessage.Headers.Add("identificador", Referencia);
                    requestMessage.Headers.Add("llave", Confi.KeyId.Trim());
                    requestMessage.Content = new StringContent(ObjCertificarJson, Encoding.UTF8, "application/json");

                    var response = await hCliente.SendAsync(requestMessage);
                    var Contenido = response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var JsonContent = JsonConvert.DeserializeObject<ResponseOK>(Contenido.Result);


                        if (JsonContent.resultado == "true")
                        {
                            ArchivoReq = JsonContent.archivo;
                            ResultadoReq = true;
                            DescripcionReq = JsonContent.descripcion;

                            var objCertificado = new ResponseOK()
                            {
                                resultado = JsonContent.resultado,                     
                                uuid = JsonContent.uuid,
                                serie = JsonContent.serie,
                                numero = JsonContent.numero,
                                xml_certificado = JsonContent.xml_certificado
                            };

                            RespuestaCertificada = objCertificado;                           
                        }
                        else
                        {
                            string ListaErrores = string.Empty;
                            foreach (var error in JsonContent.descripcion_errores)
                            {
                                ListaErrores += error.numeral;
                                ListaErrores += " : ";
                                ListaErrores += error.mensaje_error;
                                ListaErrores += " **** ";
                            }

                            ArchivoReq = JsonContent.archivo;
                            ResultadoReq = false;
                            DescripcionReq = JsonContent.descripcion;
                        }
                    }
                    else
                    {
                        ResultadoReq = false;
                    }

                }
            }

            return ResultadoReq;
        }

        public ResponseOK MiCertificacion() => RespuestaCertificada;
    }
}
