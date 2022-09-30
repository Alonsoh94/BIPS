using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class FelConfiguracion
    {
        public int Id { get; set; }
        public int? Empresa { get; set; }
        public string? NombreCertificador { get; set; }
        public string? UsuarioEmisor { get; set; }
        public string? Clave { get; set; }
        public string? Llave { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiraToken { get; set; }
        public string? UrlFirmar { get; set; }
        public string? UrlCertificar { get; set; }
        public string? UrlAnular { get; set; }
        public string? UrlToken { get; set; }
        public string? UrlRetornarDatos { get; set; }
        public string? UrlVerificarDocumento { get; set; }
        public string? UrlRetornarPdf { get; set; }
        public string? UrlRetornarXml { get; set; }
        public string? PathXml { get; set; }
        public string? PathPdfgenerado { get; set; }

        public virtual Empresa? EmpresaNavigation { get; set; }
    }
}
