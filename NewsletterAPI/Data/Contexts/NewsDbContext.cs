using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Contexts
{
    public class NewsDbContext : IdentityDbContext
    {
 
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SendNewsletterLog>()
                .HasOne<Personnel>()
                .WithMany(n => n.SendNewsletterLogs)
                .HasForeignKey(s => s.PersonnelId);

            modelBuilder.Entity<SendNewsletterLog>()
                .HasOne<Newsletter>()
                .WithMany(n => n.SendNewsletterLogs)
                .HasForeignKey(s => s.NewsletterId);
        }
    }
}
