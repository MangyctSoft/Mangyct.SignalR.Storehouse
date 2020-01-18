namespace Mangyct.SignalR.Storehouse.Web.Models
{
    /// <summary>
    /// Общая информация по продуктам
    /// </summary>
    public class ProductsInfoVM
    {
        /// <summary>
        /// Всего продуктов
        /// </summary>
        public ProductsInfoClass TotalProducts { get; set; }
        /// <summary>
        /// Общий приход продуктов
        /// </summary>
        public ProductsInfoClass TodayUpProducts { get; set; }
        /// <summary>
        /// Всего продано продуктов
        /// </summary>
        public ProductsInfoClass TodayDownProducts { get; set; }
    }

    public class ProductsInfoClass
    {
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}