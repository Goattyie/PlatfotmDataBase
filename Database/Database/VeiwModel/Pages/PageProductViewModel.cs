using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Interfaces;
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
    class PageProductViewModel:BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private Product _selectedProduct;

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
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditProduct().Show(); Execute(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj => 
                { 
                    if(_selectedProduct is not null)
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
                    Service.productMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageProductViewModel()
        {
            ProductList = new BindingList<Product>();
            Service.productMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            ProductList.Clear();
            var products =await new ProductMapper().GetAllAsync();
            foreach (var item in products)
            {
                ProductList.Add(item);
            }
        }
    }
}
