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
    class PageClientViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Client _selectedClient;

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
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditClient().Show(); }));
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
                    Service.clientMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageClientViewModel()
        {
            ClientList = new BindingList<Client>();
            Service.clientMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            ClientList.Clear();
            var clients = await new ClientMapper().GetAllAsync();
            foreach (var item in clients)
            {
                ClientList.Add(item);
            }
        }
    }
}
