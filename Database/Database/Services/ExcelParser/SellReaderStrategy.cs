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
    class SellReaderStrategy : IExcelReaderStrategy
    {
        public List<string> Titles { get; set; } = new List<string>() { "Наименование товара",  "Количество", "Прибыль", "Дата продажи", "Телефон", "Описание", "Карта" };
        public void DownloadNode(ExcelWorksheet worksheet, int row)
        {
            Product product;
            Client client = null;
            Card card = null;
            string productName = worksheet.Cells[row, 1].Text;
            var sell = new Sell();
            sell.Count = int.Parse((worksheet.Cells[row, 2].Text != "") ? worksheet.Cells[row, 2].Text : "0");
            sell.Profit = double.Parse((worksheet.Cells[row, 3].Text != "") ? worksheet.Cells[row, 3].Text : "0");
            sell.SellDate = worksheet.Cells[row, 4].Text;

            //Цена закупки и цена продажи
            var numberDirector = new NumberDirector(new NumberBuilder());
            numberDirector.Construct(worksheet.Cells[row, 3].Comment?.Text ?? "0");
            string profitText = numberDirector.Builder.ToText();
            if (profitText != null)
            {
                sell.SellCost = double.Parse(profitText.Split("-")[0]);
                sell.BuyCost = double.Parse((profitText.Split("-").Length == 2) ? (profitText.Split("-")[1] != "") ? profitText.Split("-")[1] : "0" : "0");
            }
            sell.Comment = worksheet.Cells[row, 3].Comment?.Text;

            //поиск товара в бд
            product = Service.productMapper.GetElementByName(productName);
            if (product == null)
            {
                product = new Product() { Name = productName };
                Service.productMapper.Create(product);
                product = Service.productMapper.GetElementByName(productName);
            }

            sell.ProductId = product.Id;
            //если есть номер клиента
            if (worksheet.Cells[row, 5].Text != "")
            {
                client = Service.clientMapper.GetElementByPhone(worksheet.Cells[row, 5].Text);
                if (client == null)
                {
                    client = new Client();
                    client.Phone = worksheet.Cells[row, 5].Text;
                    client.Description = worksheet.Cells[row, 6].Text + worksheet.Cells[row, 7].Text;
                    Service.clientMapper.Create(client);
                    client = Service.clientMapper.GetElementByPhone(worksheet.Cells[row, 5].Text);
                }
            }
            sell.ClientId = client?.Id;

            //если есть номер карты
            if (worksheet.Cells[row, 8].Text != "")
            {
                card = Service.cardMapper.GetElementByName(worksheet.Cells[row, 8].Text);
                if (card == null)
                {
                    card = new Card();
                    card.Name = worksheet.Cells[row, 8].Text;
                    Service.cardMapper.Create(card);
                    card = Service.cardMapper.GetElementByName(worksheet.Cells[row, 8].Text);
                }
            }
            sell.CardId = card?.Id;

            Service.sellMapper.Create(sell);
        }

        public void WriteNode(ExcelWorksheet worksheet, int row)
        {
            throw new NotImplementedException();
        }
    }
}
