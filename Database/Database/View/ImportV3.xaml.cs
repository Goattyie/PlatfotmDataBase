using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Builder;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database.View
{
    /// <summary>
    /// Логика взаимодействия для ImportV3.xaml
    /// </summary>
    public partial class ImportV3 : Window
    {
        public ImportV3()
        {
            InitializeComponent();
            V3ImporterAvailability();
            V3ImporterSell();
        }

        public void V3ImporterAvailability()
        {
            var filename = new FileInfo(@"D:\PlaygroundDataBase\Database\Database\bin\Debug\net5.0-windows\import\Наличие.xlsx");
            using (var package = new ExcelPackage(filename))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;

                string productName;
                for (int row = 2; row <= rowCount; row++)
                {
                    var availability = new Availability();

                    availability.DeliverCost = double.Parse((worksheet.Cells[row, 2].Text != "") ? worksheet.Cells[row, 2].Text : "0");
                    var numberDirector = new NumberDirector(new NumberBuilder());
                    numberDirector.Construct(worksheet.Cells[row, 2].Comment?.Text ?? "0");
                    availability.BuyCost = numberDirector.Builder.ToValue();
                    availability.Count = int.Parse((worksheet.Cells[row, 3].Text != "") ? worksheet.Cells[row, 3].Text : "0");
                    availability.SellCost = double.Parse((worksheet.Cells[row, 4].Text != "") ? worksheet.Cells[row, 4].Text : "0");

                    var profile = (worksheet.Cells[row, 5].Text != "" && worksheet.Cells[row, 5].Text != "-") ? new Profile() { Name = worksheet.Cells[row, 5].Text } : null;

                    productName = worksheet.Cells[row, 1].Text;
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
                    Service.availabilityMapper.Create(availability);
                }
            }
        }
        public void V3ImporterSell()
        {
            var filename = new FileInfo(@"D:\PlaygroundDataBase\Database\Database\bin\Debug\net5.0-windows\import\Продажи.xlsx");
            using (var package = new ExcelPackage(filename))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.End.Row;

                for (int row = 2; row <= rowCount; row++)
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
            }
        }
    }
}
