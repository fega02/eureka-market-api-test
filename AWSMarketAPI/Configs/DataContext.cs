using AWSMarket.BL.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace AWSMarketAPI.Configs
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
      
    }
}