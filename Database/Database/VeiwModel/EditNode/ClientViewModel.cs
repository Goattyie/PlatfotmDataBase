using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.EditNode
{
    class ClientViewModel:BasePropertyChanged
    {
        private Client _client;
        private BaseCommand _addCommand;
        private ClientMapper _service;

        #region Поля
        public string Phone
        {
            get { return _client.Phone; }
            set
            {
                _client.Phone = value; 
                OnPropertyChanged(nameof(_client.Phone));
            }
        }

        public string Description
        {
            get { return _client.Description; }
            set
            {
                _client.Description = value; 
                OnPropertyChanged(nameof(_client.Description));
            }
        }

        public string Car
        {
            get { return _client.Car; }
            set { _client.Car = value; OnPropertyChanged(nameof(_client.Car)); }
        }
        #endregion
        public BaseCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new BaseCommand(obj => { _service.Create(_client); _client.Id = 0; })); }
        }
        public ClientViewModel(ClientMapper service)
        {
            _client = new Client();
            _service = service;
        }
    }
}
