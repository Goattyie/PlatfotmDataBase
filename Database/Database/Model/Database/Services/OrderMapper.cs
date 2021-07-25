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
    class OrderMapper : IMapper<Order>
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

        public void Create(Order[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order[] obj)
        {
            throw new NotImplementedException();
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

        public Order GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
