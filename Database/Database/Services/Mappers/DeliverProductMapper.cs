using Database.Model.Database.Tables;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class DeliverProductMapper : Observable, IMapper<DeliverProduct>
    {
        public void Create(DeliverProduct obj)
        {
            using (var connection = new SqlModel())
            {
                    connection.DeliversProducts.Add(obj);
                    connection.SaveChanges();
            }
        }
        public void Delete(DeliverProduct[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.DeliversProducts.RemoveRange(obj);
                connection.SaveChanges();
            }
        }
        public IEnumerable<DeliverProduct> GetAll()
        {
            var dpList = new List<DeliverProduct>();
            using (var connection = new SqlModel())
            {
                dpList = connection.DeliversProducts.Include(dp => dp.Product).Include(dp => dp.Deliver).ToList();
            }
            return dpList;
        }

        public async Task<IEnumerable<DeliverProduct>> GetAllAsync()
        {
            var dpList = new List<DeliverProduct>();
            using (var connection = new SqlModel())
            {
                dpList = await connection.DeliversProducts.Include(dp => dp.Product).Include(dp => dp.Deliver).ToListAsync();
            }
            return dpList;
        }

        public IEnumerable<Product> GetProductByDeliverId(int id)
        {
            var products = new List<Product>();
            using(var connection = new SqlModel())
            {
                products = connection.DeliversProducts.Where(a => a.DeliverId == id).Select(p => p.Product).ToList();
            }
            return products;
        }
        public void Update(DeliverProduct obj)
        {
            throw new NotImplementedException();
        }
    }
}
