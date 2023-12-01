using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Contexts
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Newsletter> Newsletter { get; set; }
        public DbSet<SendNewsletterLog> SendNewsletterLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
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
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<SendNewsletterLog>()
        //  .HasOne(s => s.Personnel)
        //  .WithMany(p => p.SendNewsletterLogs)
        //  .HasForeignKey(s => s.Id);
        //    //modelBuilder.Entity<Personnel>().HasOne(s=>s.SendNewsletterLogs).WithOne(p=>p.Personnel).HasForeignKey<SendNewsletterLog>(p=>p.PersonnelId);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SendNewsletterLog>()
                .HasOne(s => s.Personnel)
                .WithMany(p => p.SendNewsletterLogs)
                .HasForeignKey(s => s.Id);

            modelBuilder.Entity<SendNewsletterLog>()
                .HasOne(s => s.Newsletter)
                .WithMany() // Assuming there is no navigation property in the Newsletter class back to SendNewsletterLog
                .HasForeignKey(s => s.Id);

            // Additional code for the Newsletter table relationship
            modelBuilder.Entity<Newsletter>()
                .HasMany(n => n.SendNewsletterLogs)
                .WithOne(s => s.Newsletter)
                .HasForeignKey(s => s.NewsletterId);
        }
    }
}
