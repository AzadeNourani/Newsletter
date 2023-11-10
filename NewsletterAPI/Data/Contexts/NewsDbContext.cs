using Microsoft.EntityFrameworkCore;
using NewsletterAPI.Data.Models;

namespace NewsletterAPI.Data.Contexts
{
    public class NewsDbContext:DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }
        public DbSet<Personnel> Personnels { get; set; }    
        public DbSet<Newsletter> Newsletter { get; set; } 
        public DbSet<SendNewsletterLog> SendNewsletterLogs { get; set;}

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
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Personnel>().HasOne(s=>s.SendNewsletterLog).WithOne(p=>p.Personnel).HasForeignKey<SendNewsletterLog>(p=>p.Id);
        }
    }
}
