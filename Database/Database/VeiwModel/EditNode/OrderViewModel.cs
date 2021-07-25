using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.EditNode
{
    class OrderViewModel:BasePropertyChanged
    {
        private Product _selectedProduct;
        private Deliver _selectedDeliver;
        private OrderMapper _service;
        private Order _order;
        private BaseCommand _executeCommand;

        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ?? (_executeCommand = new BaseCommand(obj => { _service.Create(_order); })); }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                _order.ProductId = _selectedProduct?.Id ?? 0;
                OnPropertyChanged(nameof(SelectedProduct));
                UpdateCost();
            }
        }
        public Deliver SelectedDeliver
        {
            get { return _selectedDeliver; }
            set 
            {
                _selectedDeliver = value;
                _order.DeliverId = _selectedDeliver.Id;
                SelectedProduct = null;
                var products = new DeliverProductMapper().GetProductByDeliverId(_selectedDeliver.Id);
                ProductList.Clear();
                foreach (var item in products)
                    ProductList.Add(item);
                OnPropertyChanged(nameof(SelectedDeliver));
            }
        }
        public BindingList<Product> ProductList { get; set; }
        public BindingList<Deliver> DeliverList { get; set; }

        #region Property
        public int Count
        {
            get { return _order.Count; }
            set 
            { 
                _order.Count = value; 
                OnPropertyChanged(nameof(Count));
                UpdateCost();
            }
        }
        public int CurrentCount
        {
            get { return _order.CurrentCount; }
            set { _order.CurrentCount = value; OnPropertyChanged(nameof(CurrentCount)); }
        }
        public double OrderCost
        {
            get { return _order.OrderCost; }
            set { _order.OrderCost = value; OnPropertyChanged(nameof(OrderCost)); }
        }
        public double DeliverCost
        {
            get { return _order.DeliverCost; }
            set { _order.DeliverCost = value; OnPropertyChanged(nameof(DeliverCost)); }
        }
        public double SummCost
        {
            get { return _order.SummCost; }
            set { _order.SummCost = value; OnPropertyChanged(nameof(SummCost)); }
        }
        public double CurrentCost
        {
            get { return _order.CurrentCost; }
            set { _order.CurrentCost = value; OnPropertyChanged(nameof(CurrentCost)); }
        }
        #endregion
        public OrderViewModel()
        {
            _order = new Order();
            _service = new OrderMapper();
            Count = 1;
            DeliverList = new BindingList<Deliver>();
            ProductList = new BindingList<Product>();
            var delivers = new DeliverMapper().GetAll();
            foreach (var item in delivers)
                DeliverList.Add(item);
        }
        private void UpdateCost()
        {
            if (_selectedProduct != null)
            {
                OrderCost = _selectedProduct.OrderCost * Count;
                SummCost = _selectedProduct.DeliverCost * Count;
                DeliverCost = SummCost - OrderCost;
            }
            else
            {
                OrderCost = 0;
                SummCost = 0;
                DeliverCost = 0;
            }
        }
    }
}
