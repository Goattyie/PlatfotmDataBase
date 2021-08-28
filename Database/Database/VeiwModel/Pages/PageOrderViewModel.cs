using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Interfaces;
using Database.VeiwModel.Commands;
using Database.View;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Pages
{
    class PageOrderViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _acceptCommand;
        private Order _selectedOrder;

        private double _orderSum;
        private double _deliverSum;
        private double _allSum;


        public double OrderSum
        {
            get { return _orderSum; }
            set { _orderSum = value; OnPropertyChanged(nameof(OrderSum)); }
        }
        public double DeliverSum
        {
            get { return _deliverSum; }
            set { _deliverSum = value; OnPropertyChanged(nameof(DeliverSum)); }
        }
        public double AllSum
        {
            get { return _allSum; }
            set { _allSum = value; OnPropertyChanged(nameof(AllSum)); }
        }
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); }
        }
        public ObservableCollection<Order> OrderList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditOrder().Show(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj =>
                {
                    if(_selectedOrder != null)
                        new EditOrder(_selectedOrder).Show();
                }));
            }
        }
        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Order>();
                    while (SelectedOrder != null)
                    {
                        list.Add(_selectedOrder);
                        OrderList.Remove(_selectedOrder);
                    }
                    Service.orderMapper.Delete(list.ToArray());
                }));
            }
        }
        public BaseCommand AcceptCommand
        {
            get { return _acceptCommand ?? (_acceptCommand = new BaseCommand(obj => { if(_selectedOrder != null) new AcceptOrder(_selectedOrder).ShowDialog(); })); }
        }

        public PageOrderViewModel()
        {
            OrderList = new ObservableCollection<Order>();
            Service.orderMapper.AddObserver(this);
            Service.productMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            OrderSum = 0;
            DeliverSum = 0;
            AllSum = 0;
            OrderList.Clear();
            var products = await new OrderMapper().GetAllAsync();
            products = products.OrderByDescending(x => x.Count - x.CurrentCount);
            foreach (var item in products)
            {
                OrderSum += item.OrderCost;
                DeliverSum += item.DeliverCost;
                OrderList.Add(item);
            }
            OrderList = OrderList;
            AllSum = OrderSum + DeliverSum;
        }
    }
}
