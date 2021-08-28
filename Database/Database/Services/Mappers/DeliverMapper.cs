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
    public class DeliverMapper : Observable, IMapper<Deliver>
    {
        public void Create(Deliver obj)
        {
            using (var connection = new SqlModel())
            {
                    connection.Delivers.Add(obj);
                    connection.SaveChanges();
            }
        }
        public void Delete(Deliver[] obj)
        {
            using (var connection = new SqlModel())
            {
                connection.Delivers.RemoveRange(obj);
                connection.SaveChanges();
            }
        }

        public Deliver GetElementByName(string name)
        {
            var deliver = new Deliver();
            using (var connection = new SqlModel())
            {
                deliver = connection.Delivers.Where(p => p.Name == name).FirstOrDefault();
            }
            return deliver;
        }
        public IEnumerable<Deliver> GetAll()
        {
            var delivers = new List<Deliver>();
            using (var connection = new SqlModel())
            {
                delivers = connection.Delivers.ToList();
            }
            return delivers;
        }

        public async Task<IEnumerable<Deliver>> GetAllAsync()
        {
            var delivers = new List<Deliver>();
            using (var connection = new SqlModel())
            {
                delivers = await connection.Delivers.ToListAsync();
            }
            return delivers;
        }

        public void Update(Deliver obj)
        {
            throw new NotImplementedException();
        }
    }
}
