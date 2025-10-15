using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StacktimAPI_Chloe.M.Models;

namespace StacktimAPI_Chloe.M.Data;

public partial class StacktimDbContext : DbContext
{
    public StacktimDbContext()
    {
    }

    public StacktimDbContext(DbContextOptions<StacktimDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=StacktimDb;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Players__3214EC0729045B46");

            entity.HasIndex(e => e.Email, "UQ__Players__A9D1053455C4A775").IsUnique();

            entity.HasIndex(e => e.Pseudo, "UQ__Players__F1433CEFD92A964C").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Pseudo).HasMaxLength(50);
            entity.Property(e => e.Rank).HasMaxLength(20);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams__3214EC0786A65FD0");

            entity.HasIndex(e => e.Name, "UQ__Teams__737584F6BD8725E5").IsUnique();

            entity.HasIndex(e => e.Tag, "UQ__Teams__C45164133387F148").IsUnique();

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Tag)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Captain).WithMany(p => p.Teams)
                .HasForeignKey(d => d.CaptainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team_CaptainId");
        });

        modelBuilder.Entity<TeamPlayer>(entity =>
        {
            entity.HasKey(e => new { e.TeamId, e.PlayerId });

            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Player).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.PlayerId)
                .HasConstraintName("FK_TeamPlayers_PlayerId");

            entity.HasOne(d => d.Team).WithMany(p => p.TeamPlayers)
                .HasForeignKey(d => d.TeamId)
                .HasConstraintName("FK_TeamPlayers_TeamId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
