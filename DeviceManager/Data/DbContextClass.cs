using Microsoft.EntityFrameworkCore;
using DeviceManager.Models;

namespace DeviceManager.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DeviceManager"));
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

    }
}
