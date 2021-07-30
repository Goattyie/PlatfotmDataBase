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
    public class ClientMapper : IMapper<Client>
    {
        public event Action<object> CreateEntityEvent;
        public void Create(Client obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Clients.Add(obj);
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
        public void Delete(Client[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Clients.RemoveRange(obj);
                connection.SaveChanges();
            }
        }
        public IEnumerable<Client> GetAll()
        {
            var clients = new List<Client>();
            using (var connection = new SqlModel())
            {
                clients = connection.Clients.ToList();
            }
            return clients;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var clients = new List<Client>();
            using (var connection = new SqlModel())
            {
                clients = await connection.Clients.ToListAsync();
            }
            return clients;
        }

        public void Update(Client obj)
        {
            throw new NotImplementedException();
        }
    }
}
