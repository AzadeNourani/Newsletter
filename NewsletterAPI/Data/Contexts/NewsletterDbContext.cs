using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Contexts
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
        public virtual DbSet<SendNewsletterLog> NewsletterStatuses { get; set; } = null!;
        public virtual DbSet<Personnel> Personnel { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("NewsletterDbContext");
                optionsBuilder.UseSqlServer(connectionString);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Newsletter>(entity =>
            {
                entity.ToTable("Newsletter");

                entity.Property(e => e.Id).HasColumnName("NewsletterID");

                entity.Property(e => e.SendDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<SendNewsletterLog>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Newslett__C8EE2043E96496D3");

                entity.ToTable("NewsletterStatus");

                entity.Property(e => e.Id).HasColumnName("StatusID");

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
                entity.Property(e => e.Id).HasColumnName("PersonnelID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
