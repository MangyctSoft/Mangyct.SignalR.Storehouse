using System;
using System.ComponentModel.DataAnnotations;

namespace Mangyct.SignalR.Storehouse.Database.Models
{
    /// <summary>
    /// История продукции по приходу-продажи
    /// </summary>
    public class CountStore
    {
        [Key]
        public int CountId { get; set; }
        public int ProductId { get; set; }
        public int PriceId { get; set; }
        public DateTime DateEdited { get; set; }
        public int? CountUp { get; set; }
        public int? CountDown { get; set; }
        public virtual Product Product { get; set; }
        public virtual PriceStore PriceStore { get; set; }
    }
}