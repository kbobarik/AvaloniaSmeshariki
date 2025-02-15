using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplication1.Models;

public partial class AvaloniaProjectContext : DbContext
{
    public AvaloniaProjectContext()
    {
    }

    public AvaloniaProjectContext(DbContextOptions<AvaloniaProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Smeshariki> Smesharikis { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=avaloniaProject;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.IdGender).HasName("genders_pk");

            entity.ToTable("genders");

            entity.Property(e => e.IdGender).HasColumnName("id_gender");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.IdProperty).HasName("property_pk");

            entity.ToTable("property");

            entity.Property(e => e.IdProperty).HasColumnName("id_property");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("role_pk");

            entity.ToTable("Role");

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Smeshariki>(entity =>
        {
            entity.HasKey(e => e.IdSmesharik).HasName("smeshariki_pk");

            entity.ToTable("smeshariki");

            entity.Property(e => e.IdSmesharik).HasColumnName("id_smesharik");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Animal)
                .HasColumnType("character varying")
                .HasColumnName("animal");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnType("character varying");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Smesharikis)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("smeshariki_genders_fk");

            entity.HasMany(d => d.Idproperties).WithMany(p => p.Idsmeshes)
                .UsingEntity<Dictionary<string, object>>(
                    "Smeshproperty",
                    r => r.HasOne<Property>().WithMany()
                        .HasForeignKey("Idproperty")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("smeshproperty_idproperty_fkey"),
                    l => l.HasOne<Smeshariki>().WithMany()
                        .HasForeignKey("Idsmesh")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("smeshproperty_idsmesh_fkey"),
                    j =>
                    {
                        j.HasKey("Idsmesh", "Idproperty").HasName("smeshproperty_pkey");
                        j.ToTable("smeshproperty");
                        j.IndexerProperty<int>("Idsmesh").HasColumnName("idsmesh");
                        j.IndexerProperty<int>("Idproperty").HasColumnName("idproperty");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Dateofbirtdh)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateofbirtdh");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.Patronymic)
                .HasColumnType("character varying")
                .HasColumnName("patronymic");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("users_genders_fk");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
