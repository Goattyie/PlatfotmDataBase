using Database.Model;
using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class OrderWriterStrategy : IExcelWriterStrategy
    {
        public List<string> Titles { get; set; } = new List<string>() { "Товар", "Количество", "Получено", "Цена закупки", "Цена доставки", "Оплачено", "Поставщик", "Дата заказа" };
        private Order _order;
        private List<OrderNode> _orderNodes;
        public int NodesCount { get; set; }

        public void WriteNode(ExcelWorksheet worksheet, int row)
        {
            if (NodesCount + 1 != row)
            {
                worksheet.Cells[row, 1].Value = _orderNodes[row - 2].Product.Name;
                worksheet.Cells[row, 2].Value = _orderNodes[row - 2].Count;
                worksheet.Cells[row, 3].Value = _orderNodes[row - 2].CurrentCount;
                worksheet.Cells[row, 4].Value = _orderNodes[row - 2].OrderCost;
                worksheet.Cells[row, 5].Value = _orderNodes[row - 2].DeliverCost;
                worksheet.Cells[row, 6].Value = _orderNodes[row - 2].CurrentCost;
                worksheet.Cells[row, 7].Value = _orderNodes[row - 2].Deliver.Name;
                worksheet.Cells[row, 8].Value = _orderNodes[row - 2].OrderDate.ToString("d");
            }
            else
            {
                worksheet.Cells[row, 1].Value = "Общая цена закупки:" + _order.OrderSum;
                worksheet.Cells[row, 2].Value = "Общая цена доставки:" + _order.DeliverSum;
                worksheet.Cells[row, 3].Value = "Общая цена:" + _order.AllSum;
            }
        }

        public OrderWriterStrategy(Order order)
        {
            _order = order;
            _orderNodes = order.OrderNodes.ToList();
            NodesCount = order.OrderNodes.Count + 1;
        }
    }
}
