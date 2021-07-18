using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    class ProductMapper : IMapper<Product>
    {
        public void Create(Product obj)
        {
            using(var connection = new SqlModel())
            {
                try
                {
                    connection.Products.Add(obj);
                    connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }
            }
        }

        public void Create(Product[] obj)
        {
            using (var connection = new SqlModel())
            {
                foreach(var item in obj)
                {
                    try
                    {
                        connection.Products.Add(item);
                        connection.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка");
                    }
                }
            }
        }

        public void Delete(Product obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}
