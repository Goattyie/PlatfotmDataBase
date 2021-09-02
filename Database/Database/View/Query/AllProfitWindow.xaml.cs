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
    /// Логика взаимодействия для AllProfitWindow.xaml
    /// </summary>
    public partial class AllProfitWindow : Window
    {
        public AllProfitWindow(IEnumerable<DateProfit> monthList, IEnumerable<DateProfit> yearList)
        {
            InitializeComponent();
            DataContext = new AllProfitVM(monthList, yearList);
        }
    }
}
