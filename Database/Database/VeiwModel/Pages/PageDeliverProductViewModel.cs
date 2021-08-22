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
    class PageDeliverProductViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private DeliverProduct _selectedDeliverProduct;

        public DeliverProduct SelectedDeliverProduct
        {
            get { return _selectedDeliverProduct; }
            set { _selectedDeliverProduct = value; OnPropertyChanged(nameof(SelectedDeliverProduct)); }
        }
        public BindingList<DeliverProduct> DeliverProductList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditDeliverProduct().Show(); Execute(); }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<DeliverProduct>();
                    while (SelectedDeliverProduct != null)
                    {
                        list.Add(_selectedDeliverProduct);
                        DeliverProductList.Remove(_selectedDeliverProduct);
                    }
                    Service.deliverProductMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageDeliverProductViewModel()
        {
            DeliverProductList = new BindingList<DeliverProduct>();
            Service.deliverProductMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            DeliverProductList.Clear();
            var dp =await new DeliverProductMapper().GetAllAsync();
            foreach (var item in dp)
            {
                DeliverProductList.Add(item);
            }
        }
    }
}
