using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    interface IExcelReaderStrategy
    {
        public void DownloadNode(ExcelWorksheet worksheet, int row);
    }
}
