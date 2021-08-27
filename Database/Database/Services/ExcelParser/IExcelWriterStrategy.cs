using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    interface IExcelWriterStrategy
    {
        public List<string> Titles { get; set; }
        public int NodesCount { get; set; }
        public void WriteNode(ExcelWorksheet worksheet, int row);

    }
}
