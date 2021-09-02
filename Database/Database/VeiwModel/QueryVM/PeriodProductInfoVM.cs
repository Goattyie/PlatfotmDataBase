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
    class PeriodProductInfoVM : BasePropertyChanged
    {
        public SeriesCollection Series { get; set; }
        public string[] ProductLabels { get; set; } = new string[1];
        public ObservableCollection<ProductCountProfite> ProductList { get; set; }
        public PeriodProductInfoVM(IEnumerable<ProductCountProfite> list)
        {
            ProductList = new ObservableCollection<ProductCountProfite>(list);
            Series = new SeriesCollection();

            foreach(var item in list)
            {
                Series.Add(new ColumnSeries()
                {
                    Title = item.Name,
                    Values = new ChartValues<int> { item.Count },
                });
            }
        }
    }
}
