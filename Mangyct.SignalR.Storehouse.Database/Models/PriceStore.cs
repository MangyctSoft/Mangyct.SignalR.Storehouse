using System;
using System.ComponentModel.DataAnnotations;

namespace Mangyct.SignalR.Storehouse.Database.Models
{
    /// <summary>
    /// История продукции по ценам
    /// </summary>
    public class PriceStore
    {
        [Key]
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public DateTime DateEdited { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }

    }
}