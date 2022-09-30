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

        public virtual DbSet<Adenda> Adendas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ConfiguracionesFel> ConfiguracionesFels { get; set; } = null!;
        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empresa> Empresas { get; set; } = null!;
        public virtual DbSet<Establecimiento> Establecimientos { get; set; } = null!;
        public virtual DbSet<FrasesEscenariosFiscale> FrasesEscenariosFiscales { get; set; } = null!;
        public virtual DbSet<ImpuestosPedido> ImpuestosPedidos { get; set; } = null!;
        public virtual DbSet<ItemsImpuesto> ItemsImpuestos { get; set; } = null!;
        public virtual DbSet<ItemsPedidoPv> ItemsPedidoPvs { get; set; } = null!;
        public virtual DbSet<Monedum> Moneda { get; set; } = null!;
        public virtual DbSet<Municipio> Municipios { get; set; } = null!;
        public virtual DbSet<Paise> Paises { get; set; } = null!;
        public virtual DbSet<PedidoPv> PedidoPvs { get; set; } = null!;
        public virtual DbSet<TipoDocumentoFiscal> TipoDocumentoFiscals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=JRAH-PC\\SQLEXPRESS; Database=BIPS; User ID =delta; Password=delta;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adenda>(entity =>
            {
                entity.Property(e => e.Clave)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpresaNavigation)
                    .WithMany(p => p.Adenda)
                    .HasForeignKey(d => d.Empresa)
                    .HasConstraintName("FK_Adendas_Empresa");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nit)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NIT");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MunicipioNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Municipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cliente_Cliente");
            });

            modelBuilder.Entity<ConfiguracionesFel>(entity =>
            {
                entity.ToTable("ConfiguracionesFEL");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Calve)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Certificador)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiraToken).HasColumnType("datetime");

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

                entity.Property(e => e.Urlanular)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLAnular");

                entity.Property(e => e.Urlcertificar)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLCertificar");

                entity.Property(e => e.Urlfirmar)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLFirmar");

                entity.Property(e => e.UrlretornarDatos)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLRetornarDatos");

                entity.Property(e => e.UrlretornarPdf)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLRetornarPDF");

                entity.Property(e => e.UrlretornarXml)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLRetornarXML");

                entity.Property(e => e.Urltoken)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLToken");

                entity.Property(e => e.UrlverificarDocumento)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("URLVerificarDocumento");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpresaNavigation)
                    .WithMany(p => p.ConfiguracionesFels)
                    .HasForeignKey(d => d.Empresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfiguracionesFEL_Empresa");
            });

            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.NombreDepartamento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.PaisNavigation)
                    .WithMany(p => p.Departamentos)
                    .HasForeignKey(d => d.Pais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Departamento_Paises");
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.ToTable("Empresa");

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombreComercial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroPatronal)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegimenIva)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RegimenIVA");

                entity.Property(e => e.RepresentanteLegal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rtu)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RTU");

                entity.Property(e => e.Telefono1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.MonedaBaseNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.MonedaBase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empresa_Moneda");

                entity.HasOne(d => d.MunicipioNavigation)
                    .WithMany(p => p.Empresas)
                    .HasForeignKey(d => d.Municipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empresa_Municipio");
            });

            modelBuilder.Entity<Establecimiento>(entity =>
            {
                entity.ToTable("Establecimiento");

                entity.Property(e => e.CodigoPostal)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("codigoPostal")
                    .IsFixedLength();

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreEstablecimiento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.EmpresaNavigation)
                    .WithMany(p => p.Establecimientos)
                    .HasForeignKey(d => d.Empresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Establecimiento_Empresa");

                entity.HasOne(d => d.MunicipioNavigation)
                    .WithMany(p => p.Establecimientos)
                    .HasForeignKey(d => d.Municipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Establecimiento_Municipio");
            });

            modelBuilder.Entity<FrasesEscenariosFiscale>(entity =>
            {
                entity.Property(e => e.FechaResolucion).HasColumnType("datetime");

                entity.Property(e => e.NumeroResolucion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoDocumentoFiscalNavigation)
                    .WithMany(p => p.FrasesEscenariosFiscales)
                    .HasForeignKey(d => d.TipoDocumentoFiscal)
                    .HasConstraintName("FK_FrasesEscenariosFiscales_TipoDocumentoFiscal");
            });

            modelBuilder.Entity<ImpuestosPedido>(entity =>
            {
                entity.ToTable("ImpuestosPedido");

                entity.Property(e => e.NombreCorto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalMontoImpuesto).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.PedidoNavigation)
                    .WithMany(p => p.ImpuestosPedidos)
                    .HasForeignKey(d => d.Pedido)
                    .HasConstraintName("FK_ImpuestosPedido_PedidoPV");
            });

            modelBuilder.Entity<ItemsImpuesto>(entity =>
            {
                entity.ToTable("ItemsImpuesto");

                entity.Property(e => e.ItemsPedidoPv).HasColumnName("ItemsPedidoPV");

                entity.Property(e => e.MontoGravable).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MontoImpuesto).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NombreCorto)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ItemsPedidoPvNavigation)
                    .WithMany(p => p.ItemsImpuestos)
                    .HasForeignKey(d => d.ItemsPedidoPv)
                    .HasConstraintName("FK_ItemsImpuesto_ItemsImpuesto");
            });

            modelBuilder.Entity<ItemsPedidoPv>(entity =>
            {
                entity.ToTable("ItemsPedidoPV");

                entity.Property(e => e.BienOservicio)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BienOServicio")
                    .IsFixedLength();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.PedidoPv).HasColumnName("PedidoPV");

                entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalDescuento).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalItems).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UnidadMedid)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.PedidoPvNavigation)
                    .WithMany(p => p.ItemsPedidoPvs)
                    .HasForeignKey(d => d.PedidoPv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ItemsPedidoPV_PedidoPV");
            });

            modelBuilder.Entity<Monedum>(entity =>
            {
                entity.Property(e => e.Acronimo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NombreMoneda)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.ToTable("Municipio");

                entity.Property(e => e.NombreMunicipio)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.DepartamentoNavigation)
                    .WithMany(p => p.Municipios)
                    .HasForeignKey(d => d.Departamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Municipio_Departamento");
            });

            modelBuilder.Entity<Paise>(entity =>
            {
                entity.HasIndex(e => e.Codigo, "IX_Paises")
                    .IsUnique();

                entity.Property(e => e.AcronimoPais)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PedidoPv>(entity =>
            {
                entity.ToTable("PedidoPV");

                entity.Property(e => e.AplicaIva)
                    .IsRequired()
                    .HasColumnName("AplicaIVA")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.LocalOexportacion)
                    .IsRequired()
                    .HasColumnName("LocalOExportacion")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MontoDescuento).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MontoPedido).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PorcentajeDescuento).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Referencia)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPedido).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.ClienteNavigation)
                    .WithMany(p => p.PedidoPvs)
                    .HasForeignKey(d => d.Cliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPV_Cliente");

                entity.HasOne(d => d.EstablecimientoNavigation)
                    .WithMany(p => p.PedidoPvs)
                    .HasForeignKey(d => d.Establecimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPV_Establecimiento");

                entity.HasOne(d => d.TipoDocumentoFiscalNavigation)
                    .WithMany(p => p.PedidoPvs)
                    .HasForeignKey(d => d.TipoDocumentoFiscal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PedidoPV_TipoDocumentoFiscal");
            });

            modelBuilder.Entity<TipoDocumentoFiscal>(entity =>
            {
                entity.ToTable("TipoDocumentoFiscal");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreDocumento)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nomenclatura)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
