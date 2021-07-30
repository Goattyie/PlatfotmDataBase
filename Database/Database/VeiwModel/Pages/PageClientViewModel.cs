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
    class PageClientViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Client _selectedClient;
        private ClientMapper _service;

        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }
        public BindingList<Client> ClientList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditClient(_service).Show(); DownloadData(); }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Client>();
                    while (SelectedClient != null)
                    {
                        list.Add(_selectedClient);
                        ClientList.Remove(_selectedClient);
                    }
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageClientViewModel()
        {
            ClientList = new BindingList<Client>();
            _service = new ClientMapper();
            ClientMapper.CreateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public async void DownloadData()
        {
            ClientList.Clear();
            var clients =await new ClientMapper().GetAllAsync();
            foreach (var item in clients)
            {
                ClientList.Add(item);
            }
        }
    }
}
