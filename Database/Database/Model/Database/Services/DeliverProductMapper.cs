﻿using Database.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.Model.Database.Services
{
    public class DeliverProductMapper : IMapper<DeliverProduct>
    {
        public event Action<object> CreateEntityEvent;
        public void Create(DeliverProduct obj)
        {
            using (var connection = new SqlModel())
            {
                try
                {
                    connection.DeliversProducts.Add(obj);
                    connection.SaveChanges();
                    CreateEntityEvent?.Invoke(obj);
                    MessageBox.Show("Запись добавлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Запись уже существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void Create(DeliverProduct[] obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(DeliverProduct obj)
        {
            using (var connection = new SqlModel())
            {
                connection.DeliversProducts.Remove(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(DeliverProduct[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.DeliversProducts.RemoveRange(obj);
                connection.SaveChanges();
            }
        }

        public IEnumerable<DeliverProduct> GetAll()
        {
            var dpList = new List<DeliverProduct>();
            using(var connection = new SqlModel())
            {
                dpList = (from dp in connection.DeliversProducts
                        join product in connection.Products on dp.ProductId equals product.Id
                        join deliver in connection.Delivers on dp.DeliverId equals deliver.Id
                        select new DeliverProduct()
                        {
                            Id = dp.Id,
                            ProductId = product.Id,
                            DeliverId = deliver.Id,
                            Product = new Product(product.Name),
                            Deliver = new Deliver(deliver.Name),
                        }).ToList();
            }
            return dpList;
        }

        public DeliverProduct GetElementById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DeliverProduct obj)
        {
            throw new NotImplementedException();
        }
    }
}
