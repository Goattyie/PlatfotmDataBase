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
    class ProductViewModel:ValidatePropertyChanged 
    {
        private BaseCommand _buttonClick;
        private Product _product;
        private Action _command;
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["Name"] = null,
            ["SellCost"] = null,
            ["OrderCost"] = null,
            ["DeliverCost"] = null,
        };
        #region Поля
        public string Name
        {
            get { return _product.Name; }
            set 
            { 
                _product.Name = value; 
                OnPropertyChanged(nameof(_product.Name));
                if (value?.Length == 0)
                    _errors["Name"] = "Oшибка";
                else _errors["Name"] = null;
                UpdateIsValid();
            }
        }
        public double SellCost
        {
            get { return _product.SellCost; }
            set 
            { 
                _product.SellCost = value; 
                OnPropertyChanged(nameof(_product.SellCost));
                if (value < 0)
                    _errors["SellCost"] = "Ошибка";
                else _errors["SellCost"] = null;
                UpdateIsValid();
                _product.Profit = SellCost - DeliverCost;
            }
        }    
        public double OrderCost
        {
            get { return _product.OrderCost; }
            set 
            {
                _product.OrderCost = value;
                DeliverCost = OrderCost + 0.15 * OrderCost;
                OnPropertyChanged(nameof(OrderCost));
                if (value < 0)
                    _errors["OrderCost"] = "Ошибка";
                else _errors["OrderCost"] = null;
                UpdateIsValid();
            }
        }
        public double DeliverCost
        {
            get { return _product.DeliverCost; }
            set 
            { 
                _product.DeliverCost = value; 
                OnPropertyChanged(nameof(DeliverCost));
                if (value < 0)
                    _errors["DeliverCost"] = "Ошибка";
                else _errors["DeliverCost"] = null;
                UpdateIsValid();
                _product.Profit = SellCost - DeliverCost;
            }
        }
        #endregion
        public BaseCommand AddButtonClick 
        { 
            get { return _buttonClick ??= new BaseCommand(obj => { _command?.Invoke(); }); } 
        }


        public ProductViewModel()
        {
            _product = new Product();
            _command = new Action(CreateNode);
            _errors["Name"] = "Введите название";
            UpdateIsValid();
        }

        public ProductViewModel(Product product)
        {
            _product = product;
            _command = new Action(EditNode);
        }
        private void CreateNode()
        {
            try
            {
                Service.productMapper.Create(_product);
                _product = new Product();
                Service.productMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! В записи есть ошибки либо она уже существует.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                OnPropertyChanged(nameof(_product.Name));
                OnPropertyChanged(nameof(_product.SellCost));
                OnPropertyChanged(nameof(_product.DeliverCost));
                OnPropertyChanged(nameof(_product.OrderCost));
            }
        }
        private void EditNode()
        {
            try
            {
                Service.productMapper.Update(_product);
                Service.productMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! Запись нельзя обновить таким образом.", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
