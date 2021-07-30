using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    class ProductExcelWorker : IExcelWorker<Product>
    {
        public IEnumerable<Product> Read(string filename)
        {
            string[] replaceList = new string[] { "Автор", "автор", ":", "Закупка", "закупка" };
            var list = new List<Product>();
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

                    for (int row = 2; row < rows; row++)
                    {
                        //var productItem = new Product();
                        //productItem.Name = currentWorksheet.Cells[row, 1].Text;
                        //productItem.DeliverCost = double.Parse(currentWorksheet.Cells[row, 2].Text);
                        string comment = currentWorksheet.Cells[row, 2]?.Comment?.Text ?? null;

                        foreach(var item in replaceList)
                            comment = comment?.Replace(item, " ").Trim();

                        currentWorksheet.Cells[row, 7].Value = comment;
                        
                    }
                    excelPack.Save();
                }
                catch
                {

                }

            }
            return list;
        }

        public void Write(IEnumerable<Product> obj)
        {
            throw new NotImplementedException();
        }
    }
}
