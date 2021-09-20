using Database.Model.Query;
using Database.VeiwModel.Commands;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.QueryVM
{
    class MonthProductInfoVM:BasePropertyChanged
    {
        public SeriesCollection Series { get; set; }
        public string[] MonthLabels { get; set; }
        public MonthProductInfoVM(IEnumerable<IEnumerable<MonthProductInfo>> list)
        {
            Series = new SeriesCollection();
            if (list == null)
                return;
            foreach(var product in list)
            {
                var lineSeries = new LineSeries()
                {
                    StrokeThickness = 2,
                    LineSmoothness = 0
                };
                lineSeries.Title = product.Where(x => x != null).FirstOrDefault().Name;
                lineSeries.Values = new ChartValues<int>();
                int index = 0;
                foreach(var item in product)
                {
                    if (MonthLabels == null && item != null)
                        MonthLabels = new string[product.Count()];
                    if (MonthLabels != null &&  MonthLabels[index] == null && item != null)
                        MonthLabels[index] = item.Date.ToString("MMM yyyy");

                    index++;
                    lineSeries.Values.Add(item?.Count ?? 0);
                }
                Series.Add(lineSeries);
            }
        }
    }
}
