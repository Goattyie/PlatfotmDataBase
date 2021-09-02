using Database.Model.Database.Tables;
using Database.View;
using Database.View.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Query
{
    class PeriodMonthProductInfo : Query
    {
        public override string Name { get; set; } = "За период по месяцам сравнить количество проданного товара";

        protected override void Algorithm()
        {
            var periodWindow = new InputDates();

            if (periodWindow.ShowDialog() == false)
                return;

            var changeProductsWindow = new ChangeProducts();

            if (changeProductsWindow.ShowDialog() == false)
                return;

            var firstDate = periodWindow.FirstDate.SelectedDate ?? DateTime.Now;
            var secondDate = periodWindow.SecondDate.SelectedDate ?? DateTime.Now;
            var list = changeProductsWindow.ProductListBox.Items;
            List<Product> products = new List<Product>();
            foreach (var item in list)
            {
                products.Add((Product)item);
            }
            var result = QueryService.PeriodMonthProdutInfo(firstDate, secondDate, products);
            new MonthProductInfoWindow(result).Show();
        }
    }
}
