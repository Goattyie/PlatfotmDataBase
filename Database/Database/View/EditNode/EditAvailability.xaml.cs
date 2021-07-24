using Database.Model.Database.Services;
using Database.Model.Database.Tables;
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
    /// Логика взаимодействия для EditAvailability.xaml
    /// </summary>
    public partial class EditAvailability : Window
    {
        public EditAvailability(AvailabilityMapper service)
        {
            InitializeComponent();
            DataContext = new AvailabilityViewModel(service);
        }

        public EditAvailability(AvailabilityMapper service, Availability availability)
        {
            InitializeComponent();
            DataContext = new AvailabilityViewModel(service, availability);
        }
    }
}
