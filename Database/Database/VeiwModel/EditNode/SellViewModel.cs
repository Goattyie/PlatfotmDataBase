using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.EditNode
{
    class SellViewModel:BasePropertyChanged, IDataErrorInfo
    {
        private Sell _sell;
        private SellMapper _service;
        private BaseCommand _executeCommand;
        private Action _executeDelegate;
        private Availability _selectedAvailable;
        private Card _selectedCard;
        private Client _selectedClient;
        private bool _isValid;
        private bool _isCreate;

        #region Errors
        Dictionary<string, string> _errors = new Dictionary<string, string>()
        {
            ["Count"] = null,
        };
        #endregion

        #region Списки BindingList
        public BindingList<Availability> AvailabilityList { get; set; }
        public BindingList<Card> CardList { get; set; }
        public BindingList<Client> ClientList { get; set; }
        #endregion
        #region Selected
        public Availability SelectedAvailable
        {
            get { return _selectedAvailable; }
            set
            {
                _selectedAvailable = value;
                OnPropertyChanged(nameof(SelectedAvailable));
                BuyCost = _selectedAvailable.DeliverCost;
                SellCost = _selectedAvailable.SellCost;
                Profit = Profit = (SellCost - BuyCost) * Count;
                _sell.ProductId = _selectedAvailable.ProductId;
            }
        }
        public Card SelectedCard
        {
            get { return _selectedCard; }
            set
            {
                _selectedCard = value;
                OnPropertyChanged(nameof(SelectedCard));
                _sell.CardId = _selectedCard.Id;
            }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
                _sell.ClientId = _selectedClient.Id;
            }
        }
        #endregion
        #region Поля
        public string SellDate
        {
            get { return _sell.SellDate; }
            set { _sell.SellDate = value; OnPropertyChanged(nameof(SellDate)); }
        }
        public double Profit
        {
            get { return _sell.Profit; }
            set { _sell.Profit = value; OnPropertyChanged(nameof(Profit)); }
        }
        public double SellCost
        {
            get { return _sell.SellCost; }
            set { _sell.SellCost = value; Profit = (_sell.SellCost - _sell.BuyCost) * _sell.Count; OnPropertyChanged(nameof(SellCost)); }

        }
        public double BuyCost
        {
            get { return _sell.BuyCost; }
            set { _sell.BuyCost = value; Profit = (_sell.SellCost - _sell.BuyCost) * _sell.Count; OnPropertyChanged(nameof(BuyCost)); }
        }
        
        public int Count
        {
            get { return _sell.Count; }
            set 
            { 
                _sell.Count = value;
                Profit = (_sell.SellCost - _sell.BuyCost) * _sell.Count;
                OnPropertyChanged(nameof(Count));
                if ((value < 1 || value > _selectedAvailable?.Count) && _isCreate)
                    _errors["Count"] = "Количество указано неверно";
                else _errors["Count"] = null;
                UpdateIsValid();

            }
        }

        public bool IsValid
        {
            get { return _isValid; }
            set
            {
                _isValid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }
        #endregion
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ?? (_executeCommand = new BaseCommand(obj => { _executeDelegate?.Invoke(); })); }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => _errors.ContainsKey(columnName) ? _errors[columnName] : null;

        public SellViewModel(SellMapper service)
        {
            _service = service;
            _sell = new Sell();
            _sell.SellDate = DateTime.Now.ToString().Split(" ")[0];
            AvailabilityList = new BindingList<Availability>();
            CardList = new BindingList<Card>();
            ClientList = new BindingList<Client>();

            var available = new AvailabilityMapper().GetAll();
            var cards = new CardMapper().GetAll();
            var clients = new ClientMapper().GetAll();

            foreach (var item in available)
                AvailabilityList.Add(item);
            foreach (var item in cards)
                CardList.Add(item);
            foreach (var item in clients)
                ClientList.Add(item);

            _executeDelegate = new Action(Create);
            Count = 1;
            _isCreate = true;
        }

        public SellViewModel(SellMapper service, Sell sell)
        {
            _service = service;
            _sell = sell;
            AvailabilityList = new BindingList<Availability>();
            CardList = new BindingList<Card>();
            ClientList = new BindingList<Client>();

            var available = new AvailabilityMapper().GetAll();
            var cards = new CardMapper().GetAll();
            var clients = new ClientMapper().GetAll();

            foreach (var item in available)
                AvailabilityList.Add(item);
            foreach (var item in cards)
                CardList.Add(item);
            foreach (var item in clients)
                ClientList.Add(item);

            _executeDelegate = new Action(Update);
            _isCreate = false;
            _isValid = true;

            _selectedAvailable = AvailabilityList.Where(a => a.ProductId == _sell.ProductId).FirstOrDefault();
            _selectedCard = CardList.Where(c => c.Id == _sell.CardId).FirstOrDefault();
            _selectedClient = ClientList.Where(c => c.Id == _sell.ClientId).FirstOrDefault();
        }

        private void Create()
        {
            _service.Create(_sell);
            _sell.Id = 0;
        }

        private void Update()
        {
            _service.Update(_sell);
        }

        private void UpdateIsValid()
        {
            IsValid = !_errors.Values.Any(x => x != null);
        }
    }
}
