using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.QueryVM
{
    class SellDataGridVM:BasePropertyChanged
    {
        public ObservableCollection<Sell> SellList { get; set; }
        public SellDataGridVM(IEnumerable<Sell> sellList)
        {
            SellList = new ObservableCollection<Sell>(sellList);
        }
    }
}
