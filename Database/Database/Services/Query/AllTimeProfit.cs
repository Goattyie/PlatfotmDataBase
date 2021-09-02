using Database.View.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Query
{
    class AllTimeProfit : Query
    {
        public override string Name { get; set; } = "Прибыль за все месяца и года";

        protected override void Algorithm()
        {
            var allMonthProfitList = QueryService.AllMonthProfit();
            var allYearProfitList = QueryService.AllYearProfit();
            new AllProfitWindow(allMonthProfitList, allYearProfitList).Show();
        }
    }
}
