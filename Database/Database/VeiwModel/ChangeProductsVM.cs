using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel
{
    class ChangeProductsVM:BasePropertyChanged
    {
        public ObservableCollection<Product> ProductList { get; set; }
        public Product SelectedProduct
        {
            set { 
                if(ChangedProductList.Where(x=>x.Name == value.Name).FirstOrDefault() == null)
                    ChangedProductList.Add(value); 
            }
        }
        public ObservableCollection<Product> ChangedProductList { get; set; } = new ObservableCollection<Product>();
        public ChangeProductsVM()
        {
            var list = Service.productMapper.GetAll().ToList();
            ProductList = new ObservableCollection<Product>(list.OrderBy(x=>x.Name).ToList());
        }
    }
}
