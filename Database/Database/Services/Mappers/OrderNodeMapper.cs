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
    public class OrderNodeMapper : Observable, IMapper<OrderNode>
    {
        public void Create(OrderNode obj)
        {
            using (var connection = new SqlModel())
            {
                    connection.OrderNodes.Add(obj);
                    connection.SaveChanges();
            }
        }
        public void Delete(OrderNode[] obj)
        {
            using (var connection = new SqlModel())
            {
                    connection.OrderNodes.RemoveRange(obj);
                    connection.SaveChanges();
            }
        }
        public IEnumerable<OrderNode> GetAll()
        {
            var items = new List<OrderNode>();
            using (var connection = new SqlModel())
            {
                items = connection.OrderNodes.Include(a => a.Product).Include(a => a.Deliver).ToList();
            }
            return items;
        }
        public  async  Task<IEnumerable<OrderNode>> GetAllAsync()
        {
            var items = new List<OrderNode>();
            using (var connection = new SqlModel())
            {
                items = await connection.OrderNodes.Include(a => a.Product).Include(a => a.Deliver).ToListAsync();
            }
            return items;
        }
        public void Update(OrderNode obj)
        {
            using(var connection = new SqlModel())
            {
                    connection.OrderNodes.Update(obj);
                    connection.SaveChanges();
            }
        }
        public void AcceptOrder(OrderNode obj, int count)
        {
            using(var connection = new SqlModel())
            {
                    obj.CurrentCount += count;
                    connection.OrderNodes.Update(obj);

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
