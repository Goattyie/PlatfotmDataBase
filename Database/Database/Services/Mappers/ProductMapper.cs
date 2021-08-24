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
    public class ProductMapper : Observable, IMapper<Product>
    {
        public void Create(Product obj)
        {
            using(var connection = new SqlModel())
            {
                try
                {
                    connection.Products.Add(obj);
                    connection.SaveChanges();
                    NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Ошибка добавления записи. Возможно, запись уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void Delete(Product[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Products.RemoveRange(obj);
                connection.SaveChanges();
                NotifyObserver();
            }
        }
        public IEnumerable<Product> GetAll()
        {
            var product = new List<Product>();
            using (var connection = new SqlModel())
            {
                product = connection.Products.ToList();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var product = new List<Product>();
            using (var connection = new SqlModel())
            {
                product = await connection.Products.ToListAsync();
            }
            return product;
        }

        public Product GetElementById(int id)
        {
            var product = new Product();
            using(var connection = new SqlModel())
            {
                product = connection.Products.Where(p => p.Id == id).FirstOrDefault();
            }
            return product;
        }
        public Product GetElementByName(string name)
        {
            var product = new Product();
            using (var connection = new SqlModel())
            {
                product = connection.Products.Where(p => p.Name == name).FirstOrDefault();
            }
            return product;
        }
        public void Update(Product obj)
        {
            using(var connection = new SqlModel())
            {
                connection.Products.Update(obj);
                connection.SaveChanges();
                MessageBox.Show("Запись обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NotifyObserver();
            }
        }
    }
}
