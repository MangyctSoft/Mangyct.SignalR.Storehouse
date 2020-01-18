using Mangyct.SignalR.Storehouse.Database.Repositories;
using Mangyct.SignalR.Storehouse.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace Mangyct.SignalR.Storehouse.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }       

        public PartialViewResult ProductsInfo()
        {
            ProductRepository repo = new ProductRepository();
            var products = repo.GetWithInclude(p => p.CountStores, s => s.PriceStores);
            var countStore = products.SelectMany(s => s.CountStores).ToList();
            var countPrice = products.SelectMany(s => s.PriceStores).ToList();
            
            var model = new ProductsInfoVM()
            {
                TotalProducts = new ProductsInfoClass
                {
                    Count = products.Select(s => s.Count).Sum(),
                    Price = products.Where(w => w.Count > 0).Sum(p => p.PriceStores.Where(w => w.PriceId == p.PriceId).FirstOrDefault().Price * p.Count)
                },

                TodayUpProducts = new ProductsInfoClass
                {
                    Count = products.SelectMany(s => s.CountStores).Sum(c => c.CountUp.GetValueOrDefault()),
                    Price = products
                                .SelectMany(s => s.CountStores)
                                .Where(w => w.CountUp > 0)
                                .Sum(c => c.CountUp * countPrice.FirstOrDefault(p => p.PriceId == c.PriceId).Price) ?? 0
                },

                TodayDownProducts = new ProductsInfoClass
                {
                    Count = products.SelectMany(s => s.CountStores).Sum(c => c.CountDown.GetValueOrDefault()),
                    Price = products
                                .SelectMany(s => s.CountStores)
                                .Where(w => w.CountDown > 0)
                                .Sum(c => c.CountDown * countPrice.FirstOrDefault(p => p.PriceId == c.PriceId).Price) ?? 0                       
                }
            };

            return PartialView(model);
        }

        public ActionResult ProductsDetails()
        {
            return View();
        }

        public PartialViewResult DetailsInfo()
        {
            ProductRepository repo = new ProductRepository();
            var products = repo.GetWithInclude(p => p.CountStores, s => s.PriceStores);
            var countStore = products.SelectMany(s => s.CountStores).ToList();
            var countPrice = products.SelectMany(s => s.PriceStores).ToList();

            var model = products.Select(s => new DetailsInfoVM
            {
                Name = s.Name,
                DateStoresUp = s.CountStores.Where(w => w.CountUp > 0).Select(c => new DateStore
                {
                    Date = c.DateEdited,
                    Count = c.CountUp??0,
                    Price = countPrice.FirstOrDefault(p => p.PriceId == c.PriceId).Price
                }),
                DateStoresDown = s.CountStores.Where(w => w.CountDown > 0).Select(d => new DateStore
                {
                    Date = d.DateEdited,
                    Count = d.CountDown??0,
                    Price = countPrice.FirstOrDefault(p => p.PriceId == d.PriceId).Price
                })
            });

            return PartialView(model);
        }

    }
}