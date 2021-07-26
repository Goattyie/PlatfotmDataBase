using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class ProductMapper : IMapper<Product>
    {
        public event Action<object> CreateEntityEvent;
        public event Action<object> UpdateEntityEvent;
        public void Create(Product obj)
        {
            using(var connection = new SqlModel())
            {
                try
                {
                    connection.Products.Add(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                UpdateEntityEvent?.Invoke(obj);
            }
        }
    }
}
