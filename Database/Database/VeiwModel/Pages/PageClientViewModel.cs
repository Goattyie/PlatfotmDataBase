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
    class PageClientViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _searchCommand;
        private Client _selectedClient;
        private string _searchPhone;

        public string SearchPhone
        {
            get { return _searchPhone; }
            set { _searchPhone = value; OnPropertyChanged(nameof(SearchPhone)); }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set { _selectedClient = value; OnPropertyChanged(nameof(SelectedClient)); }
        }
        public ObservableCollection<Client> ClientList { get; set; }

        public BaseCommand SearchCommand
        {
            get
            {
                return _searchCommand ??= new BaseCommand(async obj => { await FindClient(); });
            }
        }

        private async Task FindClient()
        {
            var clients = await new ClientMapper().GetAllAsync();
            clients = clients.Where(x => x.Phone.Contains(SearchPhone));
            ClientList.Clear();
            foreach (var item in clients)
            {
                ClientList.Add(item);
            }
        }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ??= new BaseCommand(obj => { new EditClient().Show(); });
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
            ClientList = new ObservableCollection<Client>();
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
