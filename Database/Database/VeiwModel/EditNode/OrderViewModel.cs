using Database.Model;
using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.VeiwModel.EditNode
{
    class OrderViewModel:ValidatePropertyChanged
    {
        private Product _selectedProduct;
        private Deliver _selectedDeliver;
        private OrderNode _orderNode;
        private BaseCommand _executeCommand;
        private Action _executeDelegate;
        private Order _order;
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["SelectedProduct"] = null,
            ["SelectedDeliver"] = null,
            ["Count"] = null,
            ["CurrentCount"] = null,
        };
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ??= new BaseCommand(obj => { _executeDelegate?.Invoke(); }); }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            {
                _selectedProduct = value;
                _orderNode.ProductId = _selectedProduct?.Id ?? 0;
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
                _orderNode.DeliverId = _selectedDeliver?.Id ?? 0;
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

        public DateTime OrderDate
        {
            get { return _orderNode.OrderDate; }
            set 
            { 
                _orderNode.OrderDate = value; 
                OnPropertyChanged(nameof(OrderDate)); 
            }
        }
        public int Count
        {
            get { return _orderNode.Count; }
            set 
            { 
                _orderNode.Count = value; 
                OnPropertyChanged(nameof(Count));
                UpdateCost();
                
                _errors["Count"] = value < 1 ? "Неверное количество": null;
                UpdateIsValid();
            }
        }
        public int CurrentCount
        {
            get { return _orderNode.CurrentCount; }
            set 
            { 
                _orderNode.CurrentCount = value; 
                OnPropertyChanged(nameof(CurrentCount));
                
                _errors["CurrentCount"] = (value < 0 || value > Count) ? "Неверное полученное количество" : null;
                UpdateIsValid();
            }
        }
        public double OrderCost
        {
            get { return _orderNode.OrderCost; }
            set 
            { 
                _orderNode.OrderCost = value; 
                OnPropertyChanged(nameof(OrderCost));
            }
        }
        public double DeliverCost
        {
            get { return _orderNode.DeliverCost; }
            set { _orderNode.DeliverCost = value; OnPropertyChanged(nameof(DeliverCost)); }
        }
        public double SummCost
        {
            get { return _orderNode.SummCost; }
            set { _orderNode.SummCost = value; OnPropertyChanged(nameof(SummCost)); }
        }
        public double CurrentCost
        {
            get { return _orderNode.CurrentCost; }
            set { _orderNode.CurrentCost = value; OnPropertyChanged(nameof(CurrentCost)); }
        }
        #endregion
        public OrderViewModel(Order order)
        {
            _order = order;
            _orderNode = new OrderNode();
            _orderNode.OrderId = order.Id;
            Count = 1;
            ProductList = new BindingList<Product>();
            var delivers = new DeliverMapper().GetAll();
            DeliverList = new BindingList<Deliver>(delivers.ToList());
            _executeDelegate = new Action(Create);
            _errors["SelectedDeliver"] = "Error";
            _errors["SelectedProduct"] = "Error";
            OrderDate = DateTime.Now;
            UpdateIsValid();
        }
        public OrderViewModel(OrderNode orderNode)
        {
            _orderNode = orderNode;
            ProductList = new BindingList<Product>();
            var delivers = new DeliverMapper().GetAll();
            DeliverList = new BindingList<Deliver>(delivers.ToList());
            _selectedDeliver = DeliverList.Where(p=>p.Id == _orderNode.DeliverId).FirstOrDefault();
            LoadProducts();
            _selectedProduct = ProductList.Where(p => p.Id == _orderNode.ProductId).FirstOrDefault();
            _executeDelegate = new Action(Update);
            IsValid = true;
        }
        private void UpdateCost()
        {
            if (_selectedProduct != null)
            {
                OrderCost = _selectedProduct.OrderCost * Count;
                DeliverCost = (_selectedProduct.DeliverCost - _selectedProduct.OrderCost) * Count;
                SummCost = OrderCost + DeliverCost;
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
            try
            {
                _orderNode.Deliver = null;
                _orderNode.Product = null;
                Service.orderNodeMapper.Create(_orderNode);
                _order.OrderNodes.Add(_orderNode);
                Service.orderMapper.ReCalculate(_order);
                Service.orderNodeMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! В записи есть ошибки либо она уже существует.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Update()
        {
            try
            {
                _orderNode.Product = null;
                _orderNode.Deliver = null;
                Service.orderNodeMapper.Update(_orderNode);

                var order = Service.orderMapper.GetAll().ToList().Find(x => x.Id == _orderNode.OrderId);
                Service.orderMapper.ReCalculate(order);
                Service.orderNodeMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! Запись нельзя обновить таким образом.", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadProducts()
        {
            var products = new DeliverProductMapper().GetProductByDeliverId(_selectedDeliver.Id).OrderBy(x=>x.Name);
            ProductList.Clear();
            foreach (var p in products)
                ProductList.Add(p);
        }
    }
}
