using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Pages
{
    class PageProductViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private Product _selectedProduct;
        private ProductMapper _service;

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }
        public BindingList<Product> ProductList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditProduct().Show(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj => 
                { 
                    new EditProduct(_selectedProduct).Show(); 
                }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Product>();
                    while (SelectedProduct != null)
                    {
                        list.Add(_selectedProduct);
                        ProductList.Remove(_selectedProduct);
                    }
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageProductViewModel()
        {
            ProductList = new BindingList<Product>();
            _service = new ProductMapper();
            var products = new ProductMapper().GetAll();
            foreach(var item in products)
            {
                ProductList.Add(item);
            }
        }
    }
}
