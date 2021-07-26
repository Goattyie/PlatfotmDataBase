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
    class AvailabilityViewModel:BasePropertyChanged
    {
        private Availability _availability;
        private AvailabilityMapper _service;
        private Product _selectedProduct;
        private Profile _selectedProfile;
        private BaseCommand _executeCommand;
        private Action _executeDelegate;

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
            set { _availability.Count = value; OnPropertyChanged(nameof(Count)); }
        }
        #endregion
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ?? (_executeCommand = new BaseCommand(obj => { _executeDelegate?.Invoke();})); }
        }
        public AvailabilityViewModel(AvailabilityMapper service)
        {
            _service = service;
            _availability = new Availability();
            ProductList = new BindingList<Product>();
            ProfileList = new BindingList<Profile>();
            var products = new ProductMapper().GetAll();
            var profiles = new ProfileMapper().GetAll();
            foreach(var item in products)
                ProductList.Add(item);
            foreach (var item in profiles)
                ProfileList.Add(item);
            _executeDelegate = new Action(Create);
        }
        public AvailabilityViewModel(AvailabilityMapper service, Availability availability)
        {
            _service = service;
            _availability = availability;
            ProductList = new BindingList<Product>();
            ProfileList = new BindingList<Profile>();
            var products = new ProductMapper().GetAll();
            var profiles = new ProfileMapper().GetAll();
            foreach (var item in products)
                ProductList.Add(item);
            foreach (var item in profiles)
                ProfileList.Add(item);
            _executeDelegate = new Action(Update);

            _selectedProduct = ProductList.Where(p => p.Id == availability.ProductId).FirstOrDefault();
            _selectedProfile = ProfileList.Where(p => p.Id == availability.ProfileId).FirstOrDefault();
        }
        private void Create()
        {
            _service.Create(_availability);
            _availability.Id = 0;
        }
        private void Update()
        {
            _service.Update(_availability);
        }
    }
}
