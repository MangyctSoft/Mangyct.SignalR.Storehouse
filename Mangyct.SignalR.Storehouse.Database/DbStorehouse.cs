using Mangyct.SignalR.Storehouse.Database.Models;
using System.Data.Entity;

namespace Mangyct.SignalR.Storehouse.Database
{
    public class DbStorehouse: DbContext
    {
        public DbStorehouse() : base("StorehouseDB")
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<PriceStore> PriceStores { get; set; }
        public DbSet<CountStore> CountStores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
