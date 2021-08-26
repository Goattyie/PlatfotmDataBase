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
    public class ClientMapper : Observable, IMapper<Client>
    {
        public void Create(Client obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.Clients.Add(obj);
                    connection.SaveChanges();
                    NotifyObserver();
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
                NotifyObserver();
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
        public Client GetElementByPhone(string phone)
        {
            Client client;
            using (var connection = new SqlModel())
            {
                client = connection.Clients.Where(x => x.Phone == phone).FirstOrDefault();
            }
            return client;
        }
    }
}
