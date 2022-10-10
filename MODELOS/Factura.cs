using System;
using System.Collections.Generic;

namespace BIPS.MODELOS
{
    public partial class Factura
    {
        public long Id { get; set; }
        public string? ReferenciaInterna { get; set; }
        public int Establecimiento { get; set; }
        public int TipoCargoCxc { get; set; }
        public int Cliente { get; set; }
        public string Nit { get; set; } = null!;
        public int Vendedor { get; set; }
        public long PedidoPv { get; set; }
        public DateTime FechaFacturacion { get; set; }
        public int Moneda { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorIva { get; set; }
        public bool Certificado { get; set; }
        public string NumeroAutorizacionC { get; set; } = null!;
        public string SerieC { get; set; } = null!;
        public string NumeroDoctoC { get; set; } = null!;
        public DateTime FechaCertificado { get; set; }
        public bool? Anulado { get; set; }
        public string? NumeroAutorizacionA { get; set; }
        public string? SerieA { get; set; }
        public string? NumeroDoctoA { get; set; }
        public DateTime? FechaAnulado { get; set; }
        public bool EstadoGeneral { get; set; }
    }
}
