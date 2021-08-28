using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Interfaces;
using Database.VeiwModel.Commands;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Pages
{
    class PageDeliverViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Deliver _selectedDeliver;

        public Deliver SelectedDeliver
        {
            get { return _selectedDeliver; }
            set { _selectedDeliver = value; OnPropertyChanged(nameof(SelectedDeliver)); }
        }
        public ObservableCollection<Deliver> DeliverList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup("Deliver").Show(); }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Deliver>();
                    while (SelectedDeliver != null)
                    {
                        list.Add(_selectedDeliver);
                        DeliverList.Remove(_selectedDeliver);
                    }
                    Service.deliverMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageDeliverViewModel()
        {
            DeliverList = new ObservableCollection<Deliver>();
            Service.deliverMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            DeliverList.Clear();
            var delivers = await new DeliverMapper().GetAllAsync();
            foreach (var item in delivers)
            {
                DeliverList.Add(item);
            }
        }
    }
}
