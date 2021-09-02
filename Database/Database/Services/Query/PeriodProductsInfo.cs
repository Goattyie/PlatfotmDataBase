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
    class PeriodProductsInfo : Query
    {
        public override string Name { get; set; } = "Сравнить количество и прибыль товаров за период";

        protected override void Algorithm()
        {
            var periodWindow = new InputDates();

            if (periodWindow.ShowDialog() == false)
                return;

            var changeProductsWindow = new ChangeProducts();

            if (changeProductsWindow.ShowDialog() == false)
                return;

            var firstDate = periodWindow.FirstDate.DisplayDate;
            var secondDate = periodWindow.SecondDate.DisplayDate;
            var list = changeProductsWindow.ProductListBox.Items;
            List<Product> products = new List<Product>();
            foreach(var item in list)
            {
                products.Add((Product)item);
            }
            var resultList = QueryService.PeriodProductInfo(firstDate, secondDate, products);
            new PeriodProductInfoWindow(resultList).Show();
        }
    }
}
