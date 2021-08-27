using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Builder;
using Database.VeiwModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Database.View
{
    /// <summary>
    /// Логика взаимодействия для ImportV3.xaml
    /// </summary>
    public partial class ImportExportDatabase : Window
    {
        public ImportExportDatabase(bool import)
        {
            InitializeComponent();
            DataContext = new VeiwModel.ImportExportDatabaseVM(this.Dispatcher, import);
        }
    }
}
