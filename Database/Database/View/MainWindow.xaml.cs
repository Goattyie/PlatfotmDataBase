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
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            new ProductExcelWorker().Read("D:\\PlaygroundDataBase\\Database\\Database\\bin\\Debug\\net5.0-windows\\import\\Товар.xls");
            var p = new Product();
            p.Name = "A";
            p.DeliverCost = 1;
            p.OrderCost = 2;
            p.SellCost = 4;
            new ProductMapper().Create(p);
        }
    }
}
