using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.EditNode
{
    class ProductViewModel:BasePropertyChanged
    {
        private BaseCommand _buttonClick;
        private readonly Product _product;
        private Action _command;
        #region Поля
        public string Name
        {
            get { return _product.Name; }
            set { _product.Name = value; OnPropertyChanged(nameof(_product.Name)); }
        }
        public double SellCost
        {
            get { return _product.SellCost; }
            set { _product.SellCost = value; OnPropertyChanged(nameof(_product.SellCost)); }
        }    
        public double OrderCost
        {
            get { return _product.OrderCost; }
            set { _product.OrderCost = value; OnPropertyChanged(nameof(_product.OrderCost)); }
        }
        public double DeliverCost
        {
            get { return _product.DeliverCost; }
            set { _product.DeliverCost = value; OnPropertyChanged(nameof(_product.OrderCost)); }
        }
        #endregion
        public BaseCommand AddButtonClick { 
            get
            {
                return _buttonClick ?? (_buttonClick = new BaseCommand(obj => { _command?.Invoke(); }));
            } }
        public ProductViewModel()
        {
            _product = new Product();
            _command = new Action(CreateNode);
        }

        public ProductViewModel(Product product)
        {
            _product = product;
            _command = new Action(EditNode);
        }
        private void CreateNode()
        {
            new ProductMapper().Create(_product);
        }
        private void EditNode()
        {
            new ProductMapper().Update(_product);
        }
    }
}
