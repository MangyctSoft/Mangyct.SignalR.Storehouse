using System;
using System.Collections.Generic;

namespace Mangyct.SignalR.Storehouse.Web.Models
{
    /// <summary>
    /// Детализация продуктов по приходу-продажи
    /// </summary>
    public class DetailsInfoVM
    {
        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список поступлений
        /// </summary>
        public IEnumerable<DateStore> DateStoresUp { get; set; }
        /// <summary>
        /// Список продаж
        /// </summary>
        public IEnumerable<DateStore> DateStoresDown { get; set; }
    }

    public class DateStore
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}