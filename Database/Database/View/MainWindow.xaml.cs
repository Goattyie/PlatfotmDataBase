
using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Builder;
using Database.VeiwModel;
using Database.View.EditNode;
using OfficeOpenXml;
using System.IO;
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
