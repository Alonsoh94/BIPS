using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BIPS.MODELOS
{
    public partial class BIPSContext : DbContext
    {
        public BIPSContext()
        {
        }

        public BIPSContext(DbContextOptions<BIPSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<FelAdenda> FelAdendas { get; set; } = null!;
        public virtual DbSet<FelConfiguracion> FelConfiguracions { get; set; } = null!;
        public virtual DbSet<FelFrase> FelFrases { get; set; } = null!;
        public virtual DbSet<FelImpuesto> FelImpuestos { get; set; } = null!;
        public virtual DbSet<FelItemsImpuesto> FelItemsImpuestos { get; set; } = null!;
        public virtual DbSet<ItemsPedido> ItemsPedidos { get; set; } = null!;
        public virtual DbSet<Moneda> Monedas { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Paise> Paises { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-1HF8VKS\\SQLEXPRESS; Database=BIPS; User ID =Developer; Password=sql;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nit)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CiudadNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Ciudad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_City");

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_State");

                entity.HasOne(d => d.PaisNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Pais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Country");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.Country)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPatronal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.RegimenIva)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RegimenIVA");

                entity.Property(e => e.RepresentanteLegal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rtu)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RTU");

                entity.Property(e => e.Telefono1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.Departamento)
                    .HasConstraintName("FK_Business_State");

                entity.HasOne(d => d.MonedaBaseNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.MonedaBase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Business_Currency");

                entity.HasOne(d => d.MunicipioNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.Municipio)
                    .HasConstraintName("FK_Business_City");

                entity.HasOne(d => d.PaisNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.Pais)
                    .HasConstraintName("FK_Business_Country");
            });

            modelBuilder.Entity<FelAdenda>(entity =>
            {
                entity.ToTable("FEL_Adendas");

                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FelConfiguracion>(entity =>
            {
                entity.ToTable("FEL_Configuracion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Clave)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiraToken).HasColumnType("datetime");

                entity.Property(e => e.Llave)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCertificador)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PathPdfgenerado)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PathPDFGenerado");

                entity.Property(e => e.PathXml)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("PathXML");

                entity.Property(e => e.Token)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UrlAnular)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_Anular");

                entity.Property(e => e.UrlCertificar)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_Certificar");

                entity.Property(e => e.UrlFirmar)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_Firmar");

                entity.Property(e => e.UrlRetornarDatos)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_RetornarDatos");

                entity.Property(e => e.UrlRetornarPdf)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_RetornarPDF");

                entity.Property(e => e.UrlRetornarXml)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_RetornarXML");

                entity.Property(e => e.UrlToken)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_Token");

                entity.Property(e => e.UrlVerificarDocumento)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URL_VerificarDocumento");

                entity.Property(e => e.UsuarioEmisor)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpresaNavigation)
                    .WithMany(p => p.FelConfiguracions)
                    .HasForeignKey(d => d.Empresa)
                    .HasConstraintName("FK_FEL_Setting_Business");
            });

            modelBuilder.Entity<FelFrase>(entity =>
            {
                entity.ToTable("FEL_Frases");

                entity.Property(e => e.FechaResolucion).HasColumnType("datetime");

                entity.Property(e => e.NumeroResolucion)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoDocumentoNavigation)
                    .WithMany(p => p.FelFrases)
                    .HasForeignKey(d => d.TipoDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FEL_Phrase_DocumentType");
            });

            modelBuilder.Entity<FelImpuesto>(entity =>
            {
                entity.ToTable("FEL_Impuestos");

                entity.Property(e => e.NombreCorto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalMontoImpuesto).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.PedidoNavigation)
                    .WithMany(p => p.FelImpuestos)
                    .HasForeignKey(d => d.Pedido)
                    .HasConstraintName("FK_FEL_Tax_FEL_Tax");
            });

            modelBuilder.Entity<FelItemsImpuesto>(entity =>
            {
                entity.ToTable("FEL_ItemsImpuestos");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MontoGravable).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.MontoImpuesto).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.NombreCorto)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ItemsPedido>(entity =>
            {
                entity.ToTable("Items_Pedidos");

                entity.Property(e => e.BienOservicio)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BienOServicio")
                    .IsFixedLength();

                entity.Property(e => e.Cantidad).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.Descuento).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Precio).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.UnidadMedida)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.PedidoNavigation)
                    .WithMany(p => p.ItemsPedidos)
                    .HasForeignKey(d => d.Pedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DetailsSaleOrder_DetailsSaleOrder");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.Property(e => e.AcronimoMoneda)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NombreMoneda)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.Property(e => e.NombreMunicipio)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.Property(e => e.AcronimoPais)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Codigo)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.AplicaIva)
                    .IsRequired()
                    .HasColumnName("AplicaIVA")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LocalOexportacion)
                    .IsRequired()
                    .HasColumnName("LocalOExportacion")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MontoDescuento).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.MontoFactura).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.MontoPedido).HasColumnType("decimal(4, 2)");

                entity.Property(e => e.PorcentajeDescuento).HasColumnType("decimal(14, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TipoPersoneria).HasMaxLength(50);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Nomenglatura)
                    .HasMaxLength(6)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
