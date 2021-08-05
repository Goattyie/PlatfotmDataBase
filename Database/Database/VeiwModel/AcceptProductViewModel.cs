using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel
{
    class AcceptProductViewModel : ValidatePropertyChanged
    {
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["Count"] = null,
        };
        private BaseCommand _acceptProductCommand;
        private OrderMapper _service;
        public BaseCommand AcceptProductCommand
        {
            get { return _acceptProductCommand ?? (_acceptProductCommand = new BaseCommand(obj => { _service.AcceptOrder(_order, _count); })); }
        }
        //Сколько получено с приезда Евгения
        private int _count;
        private Order _order;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));

                _errors["Count"] = (value < 1 || value > _order.Count - _order.CurrentCount) ? "Error" : null;
                UpdateIsValid();
            }
        }
        public string ProductName
        {
            get { return _order.Product.Name; }
        }
        public AcceptProductViewModel(OrderMapper service, Order order)
        {
            _order = order;
            _service = service;
            Count = 0;
        }
    }
}
