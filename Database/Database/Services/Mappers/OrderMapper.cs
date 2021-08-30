using Database.Model;
using Database.Model.Database;
using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Mappers
{
    class OrderMapper : Observable, IMapper<Order>
    {
        public void Create(Order obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Orders.Add(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(Order[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Orders.Remove(obj);
                connection.SaveChanges();
            }
        }

        public IEnumerable<Order> GetAll()
        {
            var list = new List<Order>();
            using (var connection = new SqlModel())
            {
                list = connection.Orders.Include(x => x.OrderNodes).ThenInclude(p => p.Product)
                    .Include(x=>x.OrderNodes).ThenInclude(d=>d.Deliver).ToList();
            }
            return list;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var list = new List<Order>();
            using(var connection = new SqlModel())
            {
                list =await connection.Orders.Include(x => x.OrderNodes).ToListAsync();
            }
            return list;
        }

        public void Update(Order obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Orders.Update(obj);
                connection.SaveChanges();
            }
        }
        public void ReCalculate(Order obj)
        {
            using (var connection = new SqlModel())
            {
                obj.OrderSum = 0;
                obj.DeliverSum = 0;
                foreach (var item in obj.OrderNodes)
                {
                    obj.OrderSum += item.OrderCost;
                    obj.DeliverSum += item.DeliverCost;
                }
                obj.AllSum = obj.DeliverSum + obj.OrderSum;
                connection.Orders.Update(obj);
                connection.SaveChanges();
            }
        }
    }
}
