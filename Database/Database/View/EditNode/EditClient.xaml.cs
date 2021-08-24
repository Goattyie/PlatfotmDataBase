using Database.Model.Database.Services;
using Database.VeiwModel.EditNode;
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

namespace Database.View.EditNode
{
    /// <summary>
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        public EditClient()
        {
            InitializeComponent();
            DataContext = new ClientViewModel();
        }
        public EditClient(string phone)
        {
            InitializeComponent();
            DataContext = new ClientViewModel(phone);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
