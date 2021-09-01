using Database.Model.Database;
using Database.Model.Database.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    static class QueryService
    {
        public static IEnumerable<Sell> SearchByPhone(string phone)
        {
            var list = new List<Sell>();
            using(var connection = new SqlModel())
            {
                list = connection.Sells
               .Include(x => x.Client)
               .Include(x=>x.Product)
               .Where(x => x.Client.Phone.Contains(phone))
               .ToList();
            }
            return list;
        }
    }
}
