using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class AvailabilityWriterStrategy : IExcelWriterStrategy
    {
        public List<string> Titles { get; set; } = new List<string>() { "Наименование товара", "Цена", "Количество", "Продажа", "Профиль" };
        public int NodesCount { get; set; }

        private List<Availability> _list;
        public AvailabilityWriterStrategy()
        {
            _list = (List<Availability>)Service.availabilityMapper.GetAll();
            NodesCount = _list.Count;
        }
        public void WriteNode(ExcelWorksheet worksheet, int row)
        {
            worksheet.Cells[row, 1].Value = _list[row - 2].Product.Name;
            worksheet.Cells[row, 2].Value = _list[row - 2].DeliverCost;
            worksheet.Cells[row, 2].AddComment(_list[row - 2].BuyCost.ToString(), "REF");
            worksheet.Cells[row, 3].Value = _list[row - 2].Count;
            worksheet.Cells[row, 4].Value = _list[row - 2].SellCost;
            worksheet.Cells[row, 5].Value = _list[row - 2].Profile?.Name;
            worksheet.Cells[row, 6].Value = _list[row - 2].Comment;
        }
    }
}
