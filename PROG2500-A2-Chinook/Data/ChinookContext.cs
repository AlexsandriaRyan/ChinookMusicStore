﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using PROG2500_A2_Chinook.Models;

namespace PROG2500_A2_Chinook.Data;

// Scaffold Command:
// scaffold-dbcontext "Data Source=(localdb)\MSSQLLocalDB;Database=Chinook;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -ContextDir Data -OutputDir Models\Generated -DataAnnotations -ContextNamespace PROG2500_A2_Chinook.Data -Namespace PROG2500_A2_Chinook.Models -force

public partial class ChinookContext : DbContext
{
    public ChinookContext()
    {
    }

    public ChinookContext(DbContextOptions<ChinookContext> options) : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ChinookConnectionString"].ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.Property(e => e.AlbumId).ValueGeneratedNever();

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Album__ArtistId__38996AB5");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.Property(e => e.ArtistId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.CustomerId).ValueGeneratedNever();

            entity.HasOne(d => d.SupportRep).WithMany(p => p.Customers).HasConstraintName("FK__Customer__Suppor__3E52440B");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.EmployeeId).ValueGeneratedNever();

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation).HasConstraintName("FK__Employee__Report__3B75D760");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.GenreId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(e => e.InvoiceId).ValueGeneratedNever();

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__Custome__4316F928");
        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.Property(e => e.InvoiceLineId).ValueGeneratedNever();

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Invoi__52593CB8");

            entity.HasOne(d => d.Track).WithMany(p => p.InvoiceLines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__InvoiceLi__Track__534D60F1");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.Property(e => e.MediaTypeId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.Property(e => e.PlaylistId).ValueGeneratedNever();

            entity.HasMany(d => d.Tracks).WithMany(p => p.Playlists)
                .UsingEntity<Dictionary<string, object>>(
                    "PlaylistTrack",
                    r => r.HasOne<Track>().WithMany()
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Track__4F7CD00D"),
                    l => l.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlaylistT__Playl__4E88ABD4"),
                    j =>
                    {
                        j.HasKey("PlaylistId", "TrackId");
                        j.ToTable("PlaylistTrack");
                        j.HasIndex(new[] { "TrackId" }, "IFK_PlaylistTrackTrackId");
                    });
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.Property(e => e.TrackId).ValueGeneratedNever();

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks).HasConstraintName("FK__Track__AlbumId__49C3F6B7");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks).HasConstraintName("FK__Track__GenreId__4AB81AF0");

            entity.HasOne(d => d.MediaType).WithMany(p => p.Tracks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Track__MediaType__4BAC3F29");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
