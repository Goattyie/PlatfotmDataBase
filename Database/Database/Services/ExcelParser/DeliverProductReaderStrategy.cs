using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class DeliverProductReaderStrategy : IExcelReaderStrategy
    {
        public void DownloadNode(ExcelWorksheet worksheet, int row)
        {
            string productName = worksheet.Cells[row, 2].Text.Trim();
            string deliverName = worksheet.Cells[row, 1].Text.Trim();

           var product =  Service.productMapper.GetElementByName(productName);
            if(product == null)
            {
                Service.productMapper.Create(new Product() { Name = productName });
                product = Service.productMapper.GetElementByName(productName);
            }

            var deliver = Service.deliverMapper.GetElementByName(deliverName);
            if(deliver == null)
            {
                Service.deliverMapper.Create(new Deliver() { Name = deliverName });
                deliver = Service.deliverMapper.GetElementByName(deliverName);
            }

            var deliverProduct = new DeliverProduct() { DeliverId = deliver.Id, ProductId = product.Id };
            Service.deliverProductMapper.Create(deliverProduct);
        }
    }
}
