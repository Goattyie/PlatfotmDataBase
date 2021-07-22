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
                var ws = excelPack.Workbook.Worksheets[0];
                int colCount = ws.Dimension.End.Column;  //get Column Count
                int rowCount = ws.Dimension.End.Row;     //get row count
                for (int row = 2; row < rowCount; row++)
                {
                    try
                    {
                        var availability = new Availability();
                        var product = new ProductMapper().GetElementByName(ws.Cells[row, 1].Text.Trim());
                        //Если такого товара не существует - создаем
                        if(product is null)
                        {
                            product = new Product();
                            product.Name = ws.Cells[row, 1].Text.Trim();
                            product.OrderCost = float.Parse(ws.Cells[row, 2].Text.Trim());
                            product.DeliverCost = float.Parse(ws.Cells[row, 3].Text.Trim());
                            product.SellCost = float.Parse(ws.Cells[row, 4].Text.Trim());
                            new ProductMapper().Create(product);
                            product = new ProductMapper().GetElementByName(ws.Cells[row, 1].Text.Trim());
                        }
                        if (ws.Cells[row, 6].Text.Trim() != "")
                        {
                            var profile = new ProfileMapper().GetElementByName(ws.Cells[row, 6].Text.Trim());
                            //Если такого профиля не сущетсвует - создаем
                            if (profile is null)
                            {
                                profile = new Profile();
                                profile.Name = ws.Cells[row, 6].Text.Trim();
                                new ProfileMapper().Create(profile);
                                profile = new ProfileMapper().GetElementByName(ws.Cells[row, 6].Text.Trim());
                            }
                            availability.Profile = profile;
                            availability.ProfileId = profile.Id;
                        }
                        availability.Product = product;
                        availability.ProductId = availability.Product.Id;
                        list.Add(availability);
                    }
                    catch
                    {

                    }
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
