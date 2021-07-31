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
    class PageDeliverProductViewModel: BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private DeliverProduct _selectedDeliverProduct;
        private DeliverProductMapper _service;

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
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditDeliverProduct(_service).Show(); DownloadData(); }));
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
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageDeliverProductViewModel()
        {
            DeliverProductList = new BindingList<DeliverProduct>();
            _service = new DeliverProductMapper();
            _service.CreateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public async void DownloadData()
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
