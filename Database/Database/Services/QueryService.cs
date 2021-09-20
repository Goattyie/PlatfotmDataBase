using Database.Model.Database;
using Database.Model.Database.Tables;
using Database.Model.Query;
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

        public static IEnumerable<DateProfit> AllMonthProfit()
        {
            var list = new List<DateProfit>();
            using (var connection = new SqlModel())
            {
                list = connection.Sells
                .Include(x => x.Product)
                .GroupBy(x => new { Month = x.SellDate.Month, Year = x.SellDate.Year })
                .Select(x => new DateProfit() { Date = DateTime.Parse($"1.{x.Key.Month}.{x.Key.Year}"), Profit = x.Sum(x => x.Profit) })
                .ToList()
                .OrderBy(x => x.Date.Year)
                 .ToList();
            }
            return list;
        }

        public static IEnumerable<DateProfit> AllYearProfit()
        {
            var list = new List<DateProfit>();
            using (var connection = new SqlModel())
            {
                list = connection.Sells
               .Include(x => x.Product)
               .GroupBy(x=>x.SellDate.Year)
               .Select(x => new DateProfit() { Date = DateTime.Parse($"1.1.{x.Key}"), Profit = x.Sum(x => x.Profit) })
               .ToList()
               .OrderBy(x => x.Date.Year)
               .ToList();
            }
            return list;
        }

        public static IEnumerable<ProductCountProfite> AllProdutcInfo()
        {
            var list = new List<ProductCountProfite>();
            using (var connection = new SqlModel())
            {
                list = connection.Sells
                    .Include(x => x.Product)
                    .GroupBy(x => x.Product.Name)
                    .Select(x => new ProductCountProfite() { Name = x.Key, Count = x.Sum(x => x.Count), Profit = x.Sum(x => x.Profit) })
                    .ToList()
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var sum = list.Sum(x => x.Count);
                var sum2 = list.Sum(x => x.Profit);
                list.ForEach(x => { x.PersentCount = Math.Round((double)x.Count / sum * 100, 2); x.PersentProfit = Math.Round((double)x.Profit / sum2 * 100, 2); });
            }
            return list;
        }
        public static IEnumerable<ProductCountProfite> PeriodProductInfo(DateTime firstDate, DateTime secondDate, IEnumerable<Product> products)
        {
            var list = new List<ProductCountProfite>();
            using (var connection = new SqlModel())
            {
                products.ToList().ForEach(x =>
                {
                    list.AddRange(connection.Sells
                    .Include(y => y.Product)
                    .Where(y => y.SellDate >= firstDate && y.SellDate <= secondDate && y.Product.Name == x.Name)
                    .GroupBy(y => y.Product.Name)
                    .Select(y => new ProductCountProfite() { Name = y.Key, Count = y.Sum(y => y.Count), Profit = y.Sum(y => y.Profit) })
                    .ToList());
                });  
            }
            list = list.OrderByDescending(x => x.Count).ToList();
            return list;
        }

        public static IEnumerable<IEnumerable<MonthProductInfo>> PeriodMonthProdutInfo(DateTime firstDate, DateTime secondDate, IEnumerable<Product> products)
        {
            var list = new List<MonthProductInfo>();
            using (var connection = new SqlModel())
            {
                products.ToList().ForEach(x =>
                {
                    list.AddRange(connection.Sells
                    .Include(s => s.Product)
                    .Where(p => p.SellDate >= firstDate && p.SellDate <= secondDate && p.Product.Name == x.Name)
                    .GroupBy(s => new { Name = s.Product.Name, Date = s.SellDate })
                    .Select(s => new MonthProductInfo() { Name = s.Key.Name, Count = s.Sum(s => s.Count), Date = DateTime.Parse($"1.{s.Key.Date.Month}.{s.Key.Date.Year}") })
                    .ToList()
                    .GroupBy(s => new { Name = s.Name, Date = s.Date })
                    .Select(s => new MonthProductInfo() { Name = s.Key.Name, Count = s.Sum(s => s.Count), Date = s.Key.Date })
                    .ToList()
                    .OrderBy(s => s.Date)
                    .ToList());
                });
            }
            //Посчитаем сколько месяцев между двумя датами
            int CalcMonth(DateTime firstDate, DateTime secondDate)
            {
                int monthCount = 1;
                if (firstDate.Month == secondDate.Month && firstDate.Year == secondDate.Year)
                    return monthCount;

                while (firstDate.AddMonths(monthCount) < secondDate) ++monthCount;
                return monthCount;
            }
            int monthCount = CalcMonth(firstDate, secondDate);
            List<MonthProductInfo[]> list2 = new List<MonthProductInfo[]>();
            if (list.Count == 0 || monthCount == 0)
                return null;

            list2.Add(new MonthProductInfo[monthCount]);
            list2[0][CalcMonth(firstDate, list.First().Date)] = list.First();

            foreach(var item in list)
            {
                bool findProduct = false;
                foreach(var productList in list2)
                {
                    if (productList.ToList().Find(x => x?.Name == item.Name) != null)
                    {
                        productList[CalcMonth(firstDate, item.Date)] = item;
                        findProduct = true;
                        break;
                    }
                }
                if (!findProduct)
                {
                    list2.Add(new MonthProductInfo[monthCount]);
                    int index = list2.Count - 1;
                    list2[index][CalcMonth(firstDate, item.Date)] = item;
                }
            }
        return list2;
    }
    }
}
