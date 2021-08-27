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
    public class SellMapper : Observable, IMapper<Sell>
    {
        public void Create(Sell obj)
        {
            using (var connection = new SqlModel())
            {
                    connection.Sells.Add(obj);
                    connection.SaveChanges();
            }
        }

        public void CreateAndUpdateAvailability(Sell obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Sells.Add(obj);
                var available = new AvailabilityMapper().GetElementByProductId(obj.ProductId);

                if (available is null)
                    throw new Exception();

                available.Count = available.Count - obj.Count;
                connection.Availability.Update(available);
                connection.SaveChanges();
            }
        }

        public void Create(Sell[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Sells.AddRange(obj);
                connection.SaveChanges();
            }
        }

        public void Delete(Sell obj)
        {
            using (var connection = new SqlModel())
            {
                try 
                {
                connection.Sells.Remove(obj);
                connection.SaveChanges();
                }
                catch
                {

                }
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
                connection.Sells.Update(obj);
                connection.SaveChanges();
            }
        }
    }
}
