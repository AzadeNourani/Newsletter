using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NewsletterAPI.Models
{
    public partial class NewsletterDbContext : DbContext
    {
        public NewsletterDbContext()
        {
        }

        public NewsletterDbContext(DbContextOptions<NewsletterDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Newsletter> Newsletters { get; set; } = null!;
        public virtual DbSet<NewsletterStatus> NewsletterStatuses { get; set; } = null!;
        public virtual DbSet<Personnel> Personnel { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Newsletter;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("Newsletter");

                entity.Property(e => e.NewsletterId).HasColumnName("NewsletterID");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<NewsletterStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__Newslett__C8EE2043E96496D3");

                entity.ToTable("NewsletterStatus");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.NewsletterId).HasColumnName("NewsletterID");

                entity.Property(e => e.PersonnelId).HasColumnName("PersonnelID");

                entity.Property(e => e.ReceiveTime).HasColumnType("datetime");

                entity.Property(e => e.SendTime).HasColumnType("datetime");

                entity.HasOne(d => d.Newsletter)
                    .WithMany(p => p.NewsletterStatuses)
                    .HasForeignKey(d => d.NewsletterId)
                    .HasConstraintName("FK__Newslette__Newsl__38996AB5");

                entity.HasOne(d => d.Personnel)
                    .WithMany(p => p.NewsletterStatuses)
                    .HasForeignKey(d => d.PersonnelId)
                    .HasConstraintName("FK__Newslette__Perso__37A5467C");
            });

            modelBuilder.Entity<Personnel>(entity =>
            {
                entity.Property(e => e.PersonnelId).HasColumnName("PersonnelID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
