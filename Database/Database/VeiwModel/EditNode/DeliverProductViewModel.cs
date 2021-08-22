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

namespace Database.VeiwModel.EditNode
{
    class DeliverProductViewModel: BasePropertyChanged
    {
        private Deliver _selectedDeliver;
        private Product _selectedProduct;
        private BaseCommand _addCommand;
        private DeliverProduct _deliverProduct;

        #region Свойства
        public Deliver SelectedDeliver
        {
            get { return _selectedDeliver; }
            set 
            { 
                _selectedDeliver = value;
                _deliverProduct.DeliverId = _selectedDeliver.Id;
                OnPropertyChanged(nameof(SelectedDeliver)); 
            }
        }
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value;
                _deliverProduct.ProductId = _selectedProduct.Id;
                OnPropertyChanged(nameof(SelectedProduct)); 
            }
        }
        public BaseCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new BaseCommand(obj => { Service.deliverProductMapper.Create(_deliverProduct); _deliverProduct.Id = 0; })); }
        }
        public BindingList<Product> ProductList { get; set; }
        public BindingList<Deliver> DeliverList { get; set; }
        #endregion
        public DeliverProductViewModel()
        {
            ProductList = new BindingList<Product>();
            DeliverList = new BindingList<Deliver>();
            _deliverProduct = new DeliverProduct();
            var products = new ProductMapper().GetAll();
            var delivers = new DeliverMapper().GetAll();

            foreach(var item in products)
            {
                ProductList.Add(item);
            }
            foreach (var item in delivers)
            {
                DeliverList.Add(item);
            }
        }
    }
}
