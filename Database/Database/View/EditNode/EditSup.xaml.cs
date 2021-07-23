using Database.Model.Database.Services;
using Database.VeiwModel.EditNode;
using Database.VeiwModel.Pages;
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
    /// Логика взаимодействия для EditSup.xaml
    /// </summary>
    public partial class EditSup : Window
    {
        public EditSup(ProfileMapper service)
        {
            InitializeComponent();
            DataContext = new ProfileViewModel(service);
        }
        public EditSup(CardMapper service)
        {
            InitializeComponent();
            DataContext = new CardViewModel(service);
        }
        public EditSup(DeliverMapper service)
        {
            InitializeComponent();
            DataContext = new DeliverViewModel(service);
        }
    }
}
