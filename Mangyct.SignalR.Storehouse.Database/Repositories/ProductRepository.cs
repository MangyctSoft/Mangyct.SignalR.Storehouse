using Mangyct.SignalR.Storehouse.Database.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Mangyct.SignalR.Storehouse.Database.Repositories
{
    public class ProductRepository : IGenericRepository<Product>
    {
        public Product Create(Product item)
        {
            using (var context = new DbStorehouse())
            {
                context.Products.Add(item);
                context.SaveChanges();
                return item;
            }
        }

        public Product FindById(int id)
        {
            using (var context = new DbStorehouse())
            {
                return context.Products.Find(id);
            }
        }

        public IEnumerable<Product> Get()
        {
            using (var context = new DbStorehouse())
            {
                return context.Products.Where(w => !w.IsDelete).AsNoTracking().ToList();
            }
        }

        public IEnumerable<Product> Get(Func<Product, bool> predicate)
        {
            using (var context = new DbStorehouse())
            {
                return context.Products.Where(w => !w.IsDelete).Where(predicate).ToList();
            }
        }

        public void Remove(int id)
        {
            using (var context = new DbStorehouse())
            {
                var product = context.Products.Find(id);
                product.IsDelete = true;
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Update(Product item)
        {
            using (var context = new DbStorehouse())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateCount(Product item, int count, bool balance)
        {
            using (var context = new DbStorehouse())
            {
                var countAdd = 
                    new CountStore
                    {
                        ProductId = item.ProductId,
                        PriceId = item.PriceId.GetValueOrDefault(),
                        DateEdited = DateTime.Now,
                        CountUp = balance ? count : 0,
                        CountDown = balance ? 0 : count
                    }
                ;
         
                //item.CountStores = countAdd;
                context.CountStores.Add(countAdd);
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdatePrice(Product item, decimal price)
        {
            using (var context = new DbStorehouse())
            {
                var priceAdd = new PriceStore()
                {                   
                        ProductId = item.ProductId,
                        DateEdited = DateTime.Now,
                        Price = price                   
                };              
                var priceStore = context.PriceStores.Add(priceAdd);
                context.SaveChanges();
                item.PriceId = priceStore.PriceId;
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetWithInclude(params Expression<Func<Product, object>>[] includeProperties)
        {
            using (var context = new DbStorehouse())
            {
                IQueryable<Product> query = context.Products.Where(w => !w.IsDelete).AsNoTracking();
                return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToList();
            }
        }

        
    }
}
