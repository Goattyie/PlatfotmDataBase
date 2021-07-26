using Database.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class OrderMapper : IMapper<Order>
    {
        public event Action<object> CreateEntityEvent;
        public event Action<object> UpdateEntityEvent;
        public void Create(Order obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Orders.Add(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка добавления записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
        public void Update(Order obj)
        {
            using(var connection = new SqlModel())
            {
                try
                {
                    connection.Orders.Update(obj);
                    connection.SaveChanges();
                    UpdateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка обновления записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
