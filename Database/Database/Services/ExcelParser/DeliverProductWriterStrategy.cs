using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class DeliverProductWriterStrategy : IExcelWriterStrategy
    {
        public List<string> Titles { get; set; } = new List<string>() { "Поставщик", "Товар" };
        private List<DeliverProduct> _list;
        public int NodesCount { get; set; }

        public void WriteNode(ExcelWorksheet worksheet, int row)
        {
            worksheet.Cells[row, 1].Value = _list[row - 2].Deliver.Name;
            worksheet.Cells[row, 2].Value = _list[row - 2].Product.Name;
        }
        public DeliverProductWriterStrategy()
        {
            _list = Service.deliverProductMapper.GetAll().ToList();
            NodesCount = _list.Count;
        }
    }
}
