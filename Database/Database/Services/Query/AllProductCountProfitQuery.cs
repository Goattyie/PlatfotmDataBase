using Database.View.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Query
{
    class AllProductCountProfitQuery : Query
    {
        public override string Name { get; set; } = "Вывести все товары, количество проданных за все время и прибыль";

        protected override void Algorithm()
        {
            var list = QueryService.AllProdutcInfo();
            new AllProductInfoWindow(list).Show();
        }
    }
}
