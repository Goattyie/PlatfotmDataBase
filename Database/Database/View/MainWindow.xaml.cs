using Database.Model.Database.ExcelWorkers;
using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel;
using OfficeOpenXml;
using System.Windows;

namespace Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }
    }
}
