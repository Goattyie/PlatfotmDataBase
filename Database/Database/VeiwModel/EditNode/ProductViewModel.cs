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
    class ProductViewModel:BasePropertyChanged, IDataErrorInfo
    {
        private BaseCommand _buttonClick;
        private Product _product;
        private readonly ProductMapper _service;
        private Action _command;
        private bool _isValid;
        private Dictionary<string, string> _errors = new Dictionary<string, string>() { 
            ["Name"] = null,
            ["SellCost"] = null,
            ["OrderCost"] = null,
            ["DeliverCost"] = null,
        };

        public bool IsValid 
        {
            get { return _isValid; }
            set 
            { 
                _isValid = value;
                OnPropertyChanged(nameof(IsValid)); 
            }
        }
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
            }
        }    
        public double OrderCost
        {
            get { return _product.OrderCost; }
            set 
            {
                _product.OrderCost = value; 
                OnPropertyChanged(nameof(_product.OrderCost));
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
                OnPropertyChanged(nameof(_product.OrderCost));
                if (value < 0)
                    _errors["DeliverCost"] = "Ошибка";
                else _errors["DeliverCost"] = null;
                UpdateIsValid();
            }
        }
        #endregion
        public BaseCommand AddButtonClick 
        { 
            get { return _buttonClick ?? (_buttonClick = new BaseCommand(obj => { _command?.Invoke(); })); } 
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName]=> _errors.ContainsKey(columnName) ? _errors[columnName] : null;

        public ProductViewModel(ProductMapper service)
        {
            _service = service;
            _product = new Product();
            _command = new Action(CreateNode);
        }

        public ProductViewModel(ProductMapper service, Product product)
        {
            _service = service;
            _product = product;
            _command = new Action(EditNode);
        }
        private void CreateNode()
        {
            _service.Create(_product);
            _product = new Product();
            OnPropertyChanged(nameof(_product.Name));
            OnPropertyChanged(nameof(_product.SellCost));
            OnPropertyChanged(nameof(_product.DeliverCost));
            OnPropertyChanged(nameof(_product.OrderCost));
        }
        private void EditNode()
        {
            _service.Update(_product);
        }

        private void UpdateIsValid()
        {
            IsValid = !_errors.Values.Any(x => x != null);
        }
    }
}
