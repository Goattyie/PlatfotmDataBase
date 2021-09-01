using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Database.VeiwModel.QueryVM
{
    class TableWindowVM:BasePropertyChanged
    {
        private Page _page;
        public Page DataGridPage
        {
            get { return _page; }
            set { _page = value; OnPropertyChanged(nameof(DataGridPage)); }
        }
        public TableWindowVM(Page page)
        {
            DataGridPage = page;
        }

    }
}
