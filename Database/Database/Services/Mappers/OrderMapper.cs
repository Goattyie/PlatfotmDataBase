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
    public class OrderMapper : Observable, IMapper<Order>
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
            using (var connection = new SqlModel())
            {
                    connection.Orders.RemoveRange(obj);
                    connection.SaveChanges();
            }
        }
        public IEnumerable<Order> GetAll()
        {
            var items = new List<Order>();
            using (var connection = new SqlModel())
            {
                items = connection.Orders.Include(a => a.Product).Include(a => a.Deliver).ToList();
            }
            return items;
        }
        public  async  Task<IEnumerable<Order>> GetAllAsync()
        {
            var items = new List<Order>();
            using (var connection = new SqlModel())
            {
                items = await connection.Orders.Include(a => a.Product).Include(a => a.Deliver).ToListAsync();
            }
            return items;
        }
        public void Update(Order obj)
        {
            using(var connection = new SqlModel())
            {
                    connection.Orders.Update(obj);
                    connection.SaveChanges();
            }
        }
        public void AcceptOrder(Order obj, int count)
        {
            using(var connection = new SqlModel())
            {
                    obj.CurrentCount += count;
                    connection.Orders.Update(obj);

                    var available = new AvailabilityMapper().GetElementByProductId(obj.ProductId);
                    
                    if (available != null)
                    {
                        available.Count += count;
                        connection.Availability.Update(available);
                    }
                    else
                    {
                        available = new Availability();
                        available.Count = count;
                        available.ProductId = obj.ProductId;
                        available.BuyCost = obj.Product.OrderCost;
                        available.DeliverCost = obj.Product.DeliverCost;
                        available.SellCost = obj.Product.SellCost;
                        connection.Availability.Add(available);
                    }

                    connection.SaveChanges();
                    MessageBox.Show("Товар принят", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
