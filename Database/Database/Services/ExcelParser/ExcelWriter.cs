using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class ExcelWriter
    {
        public event Action<int> NewIterationEvent;
        public IExcelWriterStrategy _strategy;
        public string _filename;
        public ExcelWriter(IExcelWriterStrategy strategy, string filename)
        {
            _strategy = strategy;
            _filename = filename;
        }
        public void WriteNodes()
        {
            var filename = new FileInfo(_filename);
            using (var package = new ExcelPackage(filename))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets[0];
                int rowCount = _strategy.NodesCount;

                for (int column = 1; column <= _strategy.Titles.Count; column++)
                {
                    worksheet.Cells[1, column].Value = _strategy.Titles[column - 1];
                }

                for (int row = 2; row <= rowCount + 1; row++)
                {
                    NewIterationEvent?.Invoke(row / rowCount * 100);
                    _strategy.WriteNode(worksheet, row);
                }
                package.Save();
            }
         }
    }
}
