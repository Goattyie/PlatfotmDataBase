using Database.VeiwModel.Commands;
using Database.View;
using Database.View.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Query
{
    class SearchByPhone : Query
    {
        public override string Name { get; set; } = "Вывести продажи по номеру телефона";
        protected override void Algorithm()
        {
            var inputValueWindow = new InputValue();
            if (inputValueWindow.ShowDialog() == true)
            {
                var phone = inputValueWindow.ValueTextBox.Text;
                var sellList = QueryService.SearchByPhone(phone);
                new TableWindow(new SellDataGridPage(sellList)).Show();

            }
        }
    }
}
