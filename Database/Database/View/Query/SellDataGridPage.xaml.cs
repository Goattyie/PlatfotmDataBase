using Database.Model.Database.Tables;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Database.View.Query
{
    /// <summary>
    /// Логика взаимодействия для SellDataGridPage.xaml
    /// </summary>
    public partial class SellDataGridPage : Page
    {
        public SellDataGridPage(IEnumerable<Sell> sellList)
        {
            InitializeComponent();
            DataContext = new SellDataGridVM(sellList);
        }
    }
}
