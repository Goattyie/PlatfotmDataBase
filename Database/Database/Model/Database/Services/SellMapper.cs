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
    public class SellMapper : IMapper<Sell>
    {
        public static event Action<object> CreateEntityEvent;
        public event Action<object> UpdateEntityEvent;
        public void Create(Sell obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Sells.Add(obj);
                    var available = new AvailabilityMapper().GetElementByProductId(obj.ProductId);

                    if (available is null)
                        throw new Exception();

                    available.Count = available.Count - obj.Count;
                    if (available.Count != 0)
                        connection.Availability.Update(available);
                    else connection.Availability.Remove(available);
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

        public void Create(Sell[] obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Sells.AddRange(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Записи добавлены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка создания записей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Delete(Sell obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Sells.Remove(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(Sell[] obj)
        {
            using(var connection = new SqlModel())
            {
                connection.Sells.RemoveRange(obj);
                connection.SaveChanges();
            }
        }

        public IEnumerable<Sell> GetAll()
        {
            var sells = new List<Sell>();
            using(var connection = new SqlModel())
            {
                sells = connection.Sells.Include(s => s.Product).Include(s => s.Card).Include(s => s.Client).ToList();
            }
            return sells;
        }

        public async Task<IEnumerable<Sell>> GetAllAsync()
        {
            var sells = new List<Sell>();
            using (var connection = new SqlModel())
            {
                sells = await connection.Sells.Include(s => s.Product).Include(s => s.Card).Include(s => s.Client).ToListAsync();
            }
            return sells;
        }

        public Sell GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Sell obj)
        {
            using(var connection = new SqlModel())
            {
                try
                {
                    connection.Sells.Update(obj);
                    connection.SaveChanges();
                    UpdateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись успешно обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ошибка обновления записи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
