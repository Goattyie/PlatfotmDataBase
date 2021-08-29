using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services.Interfaces;
using Database.VeiwModel.Commands;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Services;
using System.Collections.ObjectModel;

namespace Database.VeiwModel.Pages
{
    class PageSellViewModel:BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private Sell _selectedSell;
        private int _rowsCount;

        public int RowsCount
        {
            get { return _rowsCount; }
            set { _rowsCount = value; OnPropertyChanged(nameof(RowsCount)); }
        }
        public Sell SelectedSell
        {
            get { return _selectedSell; }
            set { _selectedSell = value; OnPropertyChanged(nameof(SelectedSell)); }
        }
        public ObservableCollection<Sell> SellList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSell().Show(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj =>
                {
                    if(_selectedSell is not null)
                        new EditSell(_selectedSell).Show();
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
                    Service.sellMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageSellViewModel()
        {
            SellList = new ObservableCollection<Sell>();
            Service.sellMapper.AddObserver(this);
            Service.productMapper.AddObserver(this);
            Service.clientMapper.AddObserver(this);
            Service.cardMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            SellList.Clear();
            var sell = await new SellMapper().GetAllAsync();
            sell = sell.OrderByDescending(x => x.SellDate);
            RowsCount = sell.Count();
            foreach (var item in sell)
            {
                SellList.Add(item);
            }
        }
    }
}
