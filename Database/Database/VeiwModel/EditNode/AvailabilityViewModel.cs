using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.VeiwModel.EditNode
{
    class AvailabilityViewModel:ValidatePropertyChanged
    {
        private Availability _availability;
        private Product _selectedProduct;
        private Profile _selectedProfile;
        private BaseCommand _executeCommand;
        private Action _executeDelegate;
        protected override Dictionary<string, string> _errors { get; set; } = new Dictionary<string, string>()
        {
            ["SelectedProduct"] = null,
            ["Count"] = null,
        };

        #region BindingList
        public BindingList<Product> ProductList { get; set; }
        public BindingList<Profile> ProfileList { get; set; }
        #endregion
        #region Selected
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set 
            { 
                _selectedProduct = value; 
                OnPropertyChanged(nameof(SelectedProduct));
                BuyCost = _selectedProduct.OrderCost;
                DeliverCost = _selectedProduct.DeliverCost;
                SellCost = _selectedProduct.SellCost;
                _availability.ProductId = _selectedProduct.Id;

                if (_selectedProduct is not null)
                    _errors["SelectedProduct"] = null;
                else _errors["SelectedProduct"] = "Нужно выбрать товар";
                UpdateIsValid();
            }
        }
        
        public Profile SelectedProfile
        {
            get { return _selectedProfile; }
            set
            {
                _selectedProfile = value;
                OnPropertyChanged(nameof(SelectedProfile));
                _availability.ProfileId = _selectedProfile.Id;
            }
        }
        #endregion
        #region Property
        public double BuyCost
        {
            get { return _availability.BuyCost; }
            set { _availability.BuyCost = value; OnPropertyChanged(nameof(BuyCost)); }
        }

        public double DeliverCost
        {
            get { return _availability.DeliverCost; }
            set { _availability.DeliverCost = value; OnPropertyChanged(nameof(DeliverCost)); }
        }

        public double SellCost
        {
            get { return _availability.SellCost; }
            set { _availability.SellCost = value; OnPropertyChanged(nameof(SellCost)); }
        }

        public int Count
        {
            get { return _availability.Count; }
            set 
            { 
                _availability.Count = value; 
                OnPropertyChanged(nameof(Count));

                if (_availability.Count >= 0)
                    _errors["Count"] = null;
                else _errors["Count"] = "Количество не может быть меньше 0";
                UpdateIsValid();
            }
        }

        public string Comment
        {
            get { return _availability.Comment; }
            set { _availability.Comment = value; OnPropertyChanged(nameof(Comment)); }
        }
        #endregion
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ??= new BaseCommand(obj => { _executeDelegate?.Invoke();}); }
        }
        public AvailabilityViewModel()
        {
            _availability = new Availability();
            LoadLists();
            _executeDelegate = new Action(Create);

            Count = 1;
            _errors["SelectedProduct"] = "Нужно выбрать товар";
            UpdateIsValid();
        }
        public AvailabilityViewModel(Availability availability)
        {
            _availability = availability;
            LoadLists();
            _executeDelegate = new Action(Update);

            _selectedProduct = ProductList.Where(p => p.Id == availability.ProductId).FirstOrDefault();
            _selectedProfile = ProfileList.Where(p => p.Id == availability.ProfileId).FirstOrDefault();
            UpdateIsValid();
        }
        private void LoadLists()
        {
            ProductList = new BindingList<Product>(new ProductMapper().GetAll().OrderBy(x=>x.Name).ToList());
            ProfileList = new BindingList<Profile>(new ProfileMapper().GetAll().OrderBy(x => x.Name).ToList());
        }
        private void Create()
        {
            try
            {
                _availability.Profit = _availability.SellCost - _availability.DeliverCost;
                Service.availabilityMapper.Create(_availability);
                Service.availabilityMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! В записи есть ошибки.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Update()
        {
            try
            {
                _availability.Profit = _availability.SellCost - _availability.DeliverCost; 
                _availability.Profile = null;
                Service.availabilityMapper.Update(_availability);
                Service.availabilityMapper.NotifyObserver();
            }
            catch
            {
                MessageBox.Show("Ошибка! Запись нельзя обновить таким образом.", "Ошибка обновления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
