using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    class ProductExcelWorker : IExcelWorker<Product>
    {
        public IEnumerable<Product> Read(string filename)
        {
            var list = new List<Product>();
            FileInfo existingFile = new FileInfo(filename);
            using (var excelPack = new ExcelPackage(existingFile))
            {
                var ws = excelPack.Workbook.Worksheets[0];
                int colCount = ws.Dimension.End.Column;  //get Column Count
                int rowCount = ws.Dimension.End.Row;     //get row count
                for (int row = 1; row < rowCount; row++)
                {
                    try
                    {
                        var product = new Product();
                        product.Name = ws.Cells[row, 1].Text;
                        product.OrderCost = double.Parse(ws.Cells[row, 2].Text);
                        product.DeliverCost = double.Parse(ws.Cells[row, 3].Text);
                        product.SellCost = double.Parse(ws.Cells[row, 4].Text);
                        list.Add(product);
                    }
                    catch
                    {

                    }
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
