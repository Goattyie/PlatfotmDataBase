using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using Database.View;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Pages
{
    class PageOrderViewModel: BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _acceptCommand;
        private Order _selectedOrder;
        private OrderMapper _service;

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { _selectedOrder = value; OnPropertyChanged(nameof(SelectedOrder)); }
        }
        public BindingList<Order> OrderList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditOrder(_service).Show(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj =>
                {
                    if(_selectedOrder != null)
                        new EditOrder(_service, _selectedOrder).Show();
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
                    _service.Delete(list.ToArray());
                }));
            }
        }
        public BaseCommand AcceptCommand
        {
            get { return _acceptCommand ?? (_acceptCommand = new BaseCommand(obj => { if(_selectedOrder != null) new AcceptOrder(_service, _selectedOrder).ShowDialog(); })); }
        }

        public PageOrderViewModel()
        {
            OrderList = new BindingList<Order>();
            _service = new OrderMapper();
            _service.CreateEntityEvent += OnUpdate;
            _service.UpdateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            OrderList.Clear();
            var products = new OrderMapper().GetAll();
            foreach (var item in products)
            {
                OrderList.Add(item);
            }
        }

        public async void DownloadData()
        {
            OrderList.Clear();
            var products = await new OrderMapper().GetAllAsync();
            foreach (var item in products)
            {
                OrderList.Add(item);
            }
        }
    }
}
