using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    class AvailabilityExcelWorker : IExcelWorker<Availability>
    {
        public IEnumerable<Availability> Read(string filename)
        {
            var list = new List<Availability>();
            FileInfo existingFile = new FileInfo(filename);
            using (var excelPack = new ExcelPackage(existingFile))
            {
                var workBook = excelPack.Workbook;
                if (workBook == null)
                    throw new Exception();
                try
                {
                    var currentWorksheet = workBook.Worksheets.First();
                    var rows = currentWorksheet.Dimension.End.Row;

                    for(int row = 2; row < rows; row++)
                    {
                        var availableItem = new Availability();
                        var productItem = new Product();
                    }
                }
                catch
                {

                }

            }
            return list;
        }

        public void Write(IEnumerable<Availability> obj)
        {
            throw new NotImplementedException();
        }
    }
}
