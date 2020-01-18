using System.Collections.Generic;

namespace Mangyct.SignalR.Storehouse.Database.Models
{
    /// <summary>
    /// Продукт
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int? PriceId { get; set; }
        public bool IsDelete { get; set; }

        public ICollection<CountStore> CountStores { get; set; }
        public ICollection<PriceStore> PriceStores { get; set; }

        public Product()
        {
            CountStores = new List<CountStore>();
            PriceStores = new List<PriceStore>();
        }
    }
}
