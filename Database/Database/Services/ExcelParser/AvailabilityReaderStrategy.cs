using Database.Model.Database.Tables;
using Database.Services.Builder;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class AvailabilityReaderStrategy : IExcelReaderStrategy
    {
        public void DownloadNode(ExcelWorksheet worksheet, int row)
        {
            var availability = new Availability();

            availability.DeliverCost = double.Parse((worksheet.Cells[row, 2].Text != "") ? worksheet.Cells[row, 2].Text : "0");
            var numberDirector = new NumberDirector(new NumberBuilder());
            numberDirector.Construct(worksheet.Cells[row, 2].Comment?.Text ?? "0");
            availability.BuyCost = numberDirector.Builder.ToValue();
            availability.Count = int.Parse((worksheet.Cells[row, 3].Text != "") ? worksheet.Cells[row, 3].Text : "0");
            availability.SellCost = double.Parse((worksheet.Cells[row, 4].Text != "") ? worksheet.Cells[row, 4].Text : "0");

            var profile = (worksheet.Cells[row, 5].Text != "" && worksheet.Cells[row, 5].Text != "-") ? new Profile() { Name = worksheet.Cells[row, 5].Text } : null;

            string productName = worksheet.Cells[row, 1].Text.Trim();
            var product = Service.productMapper.GetElementByName(productName) ?? new Product() { Name = productName, OrderCost = availability.BuyCost, DeliverCost = availability.DeliverCost, SellCost = availability.SellCost, Profit = availability.SellCost - availability.DeliverCost };

            if (profile != null)
                profile = Service.profileMapper.GetElementByName(profile.Name) ?? new Profile() { Name = profile.Name };

            if (Service.productMapper.GetElementByName(product?.Name) == null)
            {
                Service.productMapper.Create(product);
                product = Service.productMapper.GetElementByName(productName);
            }

            if (profile != null && Service.profileMapper.GetElementByName(profile.Name) == null)
            {
                Service.profileMapper.Create(profile);
                profile = Service.profileMapper.GetElementByName(profile.Name);
            }

            availability.ProductId = product.Id;
            availability.ProfileId = profile?.Id ?? null;
            availability.Comment = worksheet.Cells[row, 6].Text;

            if (availability.Comment != null)
            {
                availability.Comment = availability.Comment.Replace("Автор:", "");
                availability.Comment = availability.Comment.Replace("\n", "");
            }
            availability.Profit = availability.SellCost - availability.DeliverCost;
            Service.availabilityMapper.Create(availability);
        }

    }
}
