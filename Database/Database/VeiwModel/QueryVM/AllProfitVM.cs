using Database.Model.Query;
using Database.VeiwModel.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.QueryVM
{
    class AllProfitVM:BasePropertyChanged
    {
        public SeriesCollection Series { get; set; }
        public string[] MonthLabels { get; set; }
        public Func<double, string> Formatter { get; set; }
        public ObservableCollection<DateProfit> MonthProfitList { get; set; }
        public ObservableCollection<DateProfit> YearProfitList { get; set; }
        public AllProfitVM(IEnumerable<DateProfit> monthList, IEnumerable<DateProfit> yearList)
        {
            MonthProfitList = new ObservableCollection<DateProfit>(monthList);
            YearProfitList = new ObservableCollection<DateProfit>(yearList);
            Series = new SeriesCollection();
            Series.Add(
                new LineSeries()
                {
                    ToolTip = false,
           
                    Title="",
                    Values = new ChartValues<double>(monthList.Select(x=>x.Profit))
                    
                }
            );
            MonthLabels = monthList.Select(x => x.Date.ToString("MMM yyyy")).ToArray();
            Formatter = value => value.ToString("N");
        }
    }
}
