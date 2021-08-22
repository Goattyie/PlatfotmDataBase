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
    public class AvailabilityMapper :Observable, IMapper<Availability>
    {
        public void Create(Availability obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Availability.Add(obj);
                    connection.SaveChanges();
                    NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Delete(Availability[] obj)
        {
            using(var connection = new SqlModel())
            {
                connection.Availability.RemoveRange(obj);
                connection.SaveChanges();
                NotifyObserver();
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

        public async Task<IEnumerable<Availability>> GetAllAsync()
        {
            var items = new List<Availability>();
            using (var connection = new SqlModel())
            {
                items = await connection.Availability.Include(a => a.Profile).Include(a => a.Product).ToListAsync();
            }
            return items;
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
                    MessageBox.Show("Запись обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Запись не может быть обновлена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
