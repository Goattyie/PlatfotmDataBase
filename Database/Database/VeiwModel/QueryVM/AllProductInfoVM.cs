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
    class AllProductInfoVM:BasePropertyChanged
    {
        public ObservableCollection<ProductCountProfite> ProductList { get; set; }
        public AllProductInfoVM(IEnumerable<ProductCountProfite> list)
        {
            ProductList = new ObservableCollection<ProductCountProfite>(list);
        }
    }
}
