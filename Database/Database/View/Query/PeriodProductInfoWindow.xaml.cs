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
    /// Логика взаимодействия для PeriodProductInfo.xaml
    /// </summary>
    public partial class PeriodProductInfoWindow : Window
    {
        public PeriodProductInfoWindow(IEnumerable<ProductCountProfite> list)
        {
            InitializeComponent();
            DataContext = new PeriodProductInfoVM(list);
        }
    }
}
