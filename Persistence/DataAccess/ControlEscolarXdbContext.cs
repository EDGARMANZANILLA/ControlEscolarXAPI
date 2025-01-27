using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public partial class ControlEscolarXdbContext : DbContext
{
    public ControlEscolarXdbContext()
    {
    }

    public ControlEscolarXdbContext(DbContextOptions<ControlEscolarXdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatTipoPersonalTabuladorSueldo> CatTipoPersonalTabuladorSueldos { get; set; }

    public virtual DbSet<TblPersonal> TblPersonals { get; set; }

    public virtual DbSet<TblPersonalSueldo> TblPersonalSueldos { get; set; }

    public virtual DbSet<TblTipoPersonal> TblTipoPersonals { get; set; }

    public virtual DbSet<TblUsuario> TblUsuarios { get; set; }

    public virtual DbSet<VwPersonal> VwPersonals { get; set; }

    public virtual DbSet<VwTipoPersonal> VwTipoPersonals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatTipoPersonalTabuladorSueldo>(entity =>
        {
            entity.HasKey(e => e.IdCatTipoPersonalTabuladorSueldos);

            entity.ToTable("Cat_TipoPersonalTabuladorSueldos");

            entity.Property(e => e.SueldoMax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SueldoMin).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdTblTipoPersonalNavigation).WithMany(p => p.CatTipoPersonalTabuladorSueldos)
                .HasForeignKey(d => d.IdTblTipoPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cat_TipoPersonalTabuladorSueldos_Tbl_TipoPersonal");
        });

        modelBuilder.Entity<TblPersonal>(entity =>
        {
            entity.HasKey(e => e.IdTblPersonal);

            entity.ToTable("Tbl_Personal");

            entity.Property(e => e.Apellidos).HasMaxLength(70);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.NumeroControl).HasMaxLength(17);

            entity.HasOne(d => d.IdTblTipoPersonalNavigation).WithMany(p => p.TblPersonals)
                .HasForeignKey(d => d.IdTblTipoPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_Personal_Tbl_TipoPersonal");
        });

        modelBuilder.Entity<TblPersonalSueldo>(entity =>
        {
            entity.HasKey(e => e.IdTblPersonalSueldos);

            entity.ToTable("Tbl_PersonalSueldos");

            entity.Property(e => e.FechaActivo).HasColumnType("datetime");
            entity.Property(e => e.Sueldo).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdTblPersonalNavigation).WithMany(p => p.TblPersonalSueldos)
                .HasForeignKey(d => d.IdTblPersonal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tbl_PersonalSueldos_Tbl_Personal");
        });

        modelBuilder.Entity<TblTipoPersonal>(entity =>
        {
            entity.HasKey(e => e.IdTblTipoPersonal);

            entity.ToTable("Tbl_TipoPersonal");

            entity.Property(e => e.NumeroControl)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TipoPersonal).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUsuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuarios);

            entity.ToTable("Tbl_Usuarios");

            entity.Property(e => e.Contrasenia).HasMaxLength(10);
            entity.Property(e => e.FechaAlta).HasColumnType("datetime");
            entity.Property(e => e.NombreUsuario).HasMaxLength(10);
        });

        modelBuilder.Entity<VwPersonal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwPersonal");

            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.NombreCompleto).HasMaxLength(121);
            entity.Property(e => e.NumeroControl).HasMaxLength(17);
            entity.Property(e => e.Sueldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoPersonal).HasMaxLength(50);
        });

        modelBuilder.Entity<VwTipoPersonal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VwTipoPersonal");

            entity.Property(e => e.NumeroControl)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SueldoMax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SueldoMin).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoPersonal).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
