using Database.Model.Query;
using Database.VeiwModel.QueryVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database.View.Query
{
    /// <summary>
    /// Логика взаимодействия для MonthProductInfoWindow.xaml
    /// </summary>
    public partial class MonthProductInfoWindow : Window
    {
        public MonthProductInfoWindow(IEnumerable<IEnumerable<MonthProductInfo>> list)
        {
            InitializeComponent();
            DataContext = new MonthProductInfoVM(list);
        }
    }
}
