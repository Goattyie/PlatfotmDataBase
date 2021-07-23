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
    class PageDeliverViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Deliver _selectedDeliver;
        private DeliverMapper _service;

        public Deliver SelectedDeliver
        {
            get { return _selectedDeliver; }
            set { _selectedDeliver = value; OnPropertyChanged(nameof(SelectedDeliver)); }
        }
        public BindingList<Deliver> DeliverList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup(_service).Show(); DownloadData(); }));
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
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageDeliverViewModel()
        {
            DeliverList = new BindingList<Deliver>();
            _service = new DeliverMapper();
            _service.CreateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public void DownloadData()
        {
            DeliverList.Clear();
            var delivers = new DeliverMapper().GetAll();
            foreach (var item in delivers)
            {
                DeliverList.Add(item);
            }
        }
    }
}
