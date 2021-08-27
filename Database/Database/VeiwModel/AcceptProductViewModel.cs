using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.VeiwModel
{
    class AcceptProductViewModel : ValidatePropertyChanged
    {
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["Count"] = null,
        };
        private BaseCommand _acceptProductCommand;
        public BaseCommand AcceptProductCommand
        {
            get { return _acceptProductCommand ??= new BaseCommand(obj => 
            {
                try
                {
                    Service.orderMapper.AcceptOrder(_order, _count);
                    Service.orderMapper.NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Ошибка принятия товара.", "Ошибка принятия товара", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }); }
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
        public AcceptProductViewModel(Order order)
        {
            _order = order;
            Count = 0;
        }
    }
}
