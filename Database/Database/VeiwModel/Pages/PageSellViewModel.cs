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
    class PageSellViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private Sell _selectedSell;
        private SellMapper _service;

        public Sell SelectedSell
        {
            get { return _selectedSell; }
            set { _selectedSell = value; OnPropertyChanged(nameof(SelectedSell)); }
        }
        public BindingList<Sell> SellList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSell(_service).Show(); DownloadData(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj =>
                {
                    if(_selectedSell is not null)
                        new EditSell(_service, _selectedSell).Show();
                }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Sell>();
                    while (SelectedSell != null)
                    {
                        list.Add(_selectedSell);
                        SellList.Remove(_selectedSell);
                    }
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageSellViewModel()
        {
            SellList = new BindingList<Sell>();
            _service = new SellMapper();
            SellMapper.CreateEntityEvent += OnUpdate;
            _service.UpdateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public void DownloadData()
        {
            SellList.Clear();
            var products = new SellMapper().GetAll();
            foreach (var item in products)
            {
                SellList.Add(item);
            }
        }
    }
}
