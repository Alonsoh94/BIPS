using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class ConfiguracionesFel
    {
        public int Id { get; set; }
        public int Empresa { get; set; }
        public string Certificador { get; set; } = null!;
        public string? Usuario { get; set; }
        public string? Calve { get; set; }
        public string? KeyId { get; set; }
        public string? Token { get; set; }
        public DateTime? ExpiraToken { get; set; }
        public string? Urlfirmar { get; set; }
        public string? Urlcertificar { get; set; }
        public string? Urlanular { get; set; }
        public string? Urltoken { get; set; }
        public string? UrlretornarDatos { get; set; }
        public string? UrlverificarDocumento { get; set; }
        public string? UrlretornarPdf { get; set; }
        public string? UrlretornarXml { get; set; }
        public string? PathXml { get; set; }
        public string? PathPdfgenerado { get; set; }
        public string? UrlimprimirInfile { get; set; }
        public string? CorreoCopia { get; set; }

        public virtual Empresa EmpresaNavigation { get; set; } = null!;
    }
}
