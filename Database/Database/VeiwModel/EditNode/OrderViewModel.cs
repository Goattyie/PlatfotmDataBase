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
    class OrderViewModel:ValidatePropertyChanged
    {
        private Product _selectedProduct;
        private Deliver _selectedDeliver;
        private OrderMapper _service;
        private Order _order;
        private BaseCommand _executeCommand;
        private Action _executeDelegate;
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["SelectedProduct"] = null,
            ["SelectedDeliver"] = null,
            ["Count"] = null,
            ["CurrentCount"] = null,
        };
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ?? (_executeCommand = new BaseCommand(obj => { _executeDelegate?.Invoke(); })); }
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

                _errors["SelectedProduct"] = _selectedProduct  == null ? "Неверный товар" : null;             
                UpdateIsValid();
            }
        }
        public Deliver SelectedDeliver
        {
            get { return _selectedDeliver; }
            set 
            {
                _selectedDeliver = value;
                _order.DeliverId = _selectedDeliver?.Id ?? 0;
                SelectedProduct = null;
                LoadProducts();
                OnPropertyChanged(nameof(SelectedDeliver));


                _errors["SelectedDeliver"] = _selectedDeliver == null ? "Неверный поставщик" : null;
                UpdateIsValid();
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
                
                _errors["Count"] = value < 1 ? "Неверное количество": null;
                UpdateIsValid();
            }
        }
        public int CurrentCount
        {
            get { return _order.CurrentCount; }
            set 
            { 
                _order.CurrentCount = value; 
                OnPropertyChanged(nameof(CurrentCount));
                
                _errors["CurrentCount"] = (value < 1 || value > Count) ? "Неверное полученное количество" : null;
                UpdateIsValid();
            }
        }
        public double OrderCost
        {
            get { return _order.OrderCost; }
            set 
            { 
                _order.OrderCost = value; 
                OnPropertyChanged(nameof(OrderCost));
            }
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
        public OrderViewModel(OrderMapper service)
        {
            _order = new Order();
            _service = service;
            Count = 1;
            DeliverList = new BindingList<Deliver>();
            ProductList = new BindingList<Product>();
            var delivers = new DeliverMapper().GetAll();
            foreach (var item in delivers)
                DeliverList.Add(item);

            _executeDelegate = new Action(Create);
            _errors["SelectedDeliver"] = "Error";
            _errors["SelectedProduct"] = "Error";
            UpdateIsValid();
        }
        public OrderViewModel(OrderMapper service, Order order)
        {
            _order = order;
            _service = service;
            DeliverList = new BindingList<Deliver>();
            ProductList = new BindingList<Product>();
            var delivers = new DeliverMapper().GetAll();
            foreach (var item in delivers)
                DeliverList.Add(item);

            _selectedDeliver = DeliverList.Where(p=>p.Id == _order.DeliverId).FirstOrDefault();
            LoadProducts();
            _selectedProduct = ProductList.Where(p => p.Id == _order.ProductId).FirstOrDefault();
            _executeDelegate = new Action(Update);
            IsValid = true;
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
        private void Create()
        {
            _order.Deliver = null;
            _order.Product = null;
            _service.Create(_order);
            _order.Id = 0;
        }
        private void Update()
        {
            _service.Update(_order);
        }
        private void LoadProducts()
        {
            var products = new DeliverProductMapper().GetProductByDeliverId(_selectedDeliver.Id);
            ProductList.Clear();
            foreach (var item in products)
                ProductList.Add(item);
        }
    }
}
