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
    public class AvailabilityMapper : IMapper<Availability>
    {
        public event Action<object> CreateEntityEvent;
        public event Action<object> UpdateEntityEvent;
        public void Create(Availability obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Availability.Add(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Create(Availability[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Availability obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Availability.Remove(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(Availability[] obj)
        {
            using(var connection = new SqlModel())
            {
                connection.Availability.RemoveRange(obj);
                connection.SaveChanges();
            }
        }

        public IEnumerable<Availability> GetAll()
        {
            var items = new List<Availability>();
            using(var connection = new SqlModel())
            {
                items = connection.Availability.Include(a=>a.Profile).Include(a=>a.Product).ToList();
            }
            return items;
        }

        public Availability GetElementById(int id)
        {
            throw new NotImplementedException();
        }
        public Availability GetElementByProductId(int id)
        {
            var available = new Availability();
            using(var connection = new SqlModel())
            {
                available = connection.Availability.Where(a => a.ProductId == id).FirstOrDefault();
            }
            return available;
        }

        public void Update(Availability obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Availability.Update(obj);
                    connection.SaveChanges();
                    UpdateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Запись не может быть обновлена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
