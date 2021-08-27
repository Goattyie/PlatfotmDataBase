using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class ExcelReader
    {
        public event Action<int> NewIterationEvent;
        public event Action<string, int> ErrorDownloadEvent;
        public IExcelReaderStrategy _strategy;
        public string _filename;
        public ExcelReader(IExcelReaderStrategy strategy, string filename)
        {
            _strategy = strategy;
            _filename = filename;
        }
        public void DownloadNodes()
        {
            var filename = new FileInfo(_filename);
            using (var package = new ExcelPackage(filename))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
                {
                    try
                    {
                        NewIterationEvent?.Invoke((int)((double)row / rowCount * 100));
                        _strategy.DownloadNode(worksheet, row);
                    }
                    catch
                    {
                        ErrorDownloadEvent?.Invoke(_filename, row);
                    }
                }
            }
        }
    }
}
