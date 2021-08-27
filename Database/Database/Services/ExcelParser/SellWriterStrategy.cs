using Database.Model.Database.Tables;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.ExcelParser
{
    class SellWriterStrategy : IExcelWriterStrategy
    {
        public List<string> Titles { get; set; } = new List<string>() { "Наименование товара", "Количество", "Прибыль", "Дата продажи", "Телефон", "Описание", "Доп информация", "Карта" };
        public int NodesCount { get; set; }
        private List<Sell> _list;

        public SellWriterStrategy()
        {
            _list = (List<Sell>)Service.sellMapper.GetAll();
            NodesCount = _list.Count;
        }

        public void WriteNode(ExcelWorksheet worksheet, int row)
        {
            worksheet.Cells[row, 1].Value = _list[row - 2].Product.Name;
            worksheet.Cells[row, 2].Value = _list[row - 2].Count;
            worksheet.Cells[row, 3].Value = _list[row - 2].Profit;
            worksheet.Cells[row, 3].AddComment($"{_list[row - 2].SellCost} - {_list[row - 2].BuyCost}", "REF");
            worksheet.Cells[row, 4].Value = _list[row - 2].SellDate;
            worksheet.Cells[row, 5].Value = _list[row - 2].Client?.Phone;
            worksheet.Cells[row, 6].Value = _list[row - 2].Client?.Description;
            worksheet.Cells[row, 8].Value = _list[row - 2].Card?.Name;
        }
    }
}
