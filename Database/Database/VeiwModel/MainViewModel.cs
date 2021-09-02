using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Database.Model;
using Database.Services.Query;
using Database.VeiwModel.Commands;
using Database.View;
using Database.View.Pages;
using Database.View.Pages.Tables;

namespace Database.VeiwModel
{
    class MainViewModel : BasePropertyChanged
    {
        private Page _currentPage;
        private Page _availabilityPage;
        private Page _clientPage;
        private Page _productPage;
        private Page _deliverPage;
        private Page _deliverProductPage;
        private Page _sellPage;
        private Page _profilePage;
        private Page _cardPage;

        private BaseCommand _openOrderCommand;
        private BaseCommand _tableClick;
        private BaseCommand _exportDatabase;
        private BaseCommand _importDatabase;

        public ObservableCollection<Query> QueryList { get; set; }
        public BaseCommand OpenOrderCommand
        {
            get { return _openOrderCommand ??= new BaseCommand((obj) => { new OrderWindow(bool.Parse(obj.ToString())).Show(); }); }
        }
        public BaseCommand ImportDatabase
        {
            get { return _importDatabase ?? (_importDatabase = new BaseCommand(obj => { new ImportExportDatabase(true).Show(); })); }
        }

        public BaseCommand TableClick
        {
            get
            {
                return _tableClick ??
                  (_tableClick = new BaseCommand((obj) => { NewTablePage(obj); }));
            }
        }
        public BaseCommand ExportDatabase 
        {
            get { return _exportDatabase ?? (_exportDatabase = new BaseCommand(obj => { new ImportExportDatabase(false).Show(); })); }
        }
        public Page CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }
        public MainViewModel()
        {
            _sellPage = new Sell();
            _availabilityPage = new Availability();
            _clientPage = new Client();
            _productPage = new Product();
            _deliverPage = new Deliver();
            _deliverProductPage = new DeliverProduct();
            _profilePage = new Profile();
            _cardPage = new Card();

            _currentPage = _sellPage;
            AddQueryes();
        }

        private void AddQueryes()
        {
            QueryList = new ObservableCollection<Query>();
            QueryList.Add(new SearchByPhone());
            QueryList.Add(new PeriodMonthProductInfo());
            QueryList.Add(new PeriodProductsInfo());
            QueryList.Add(new AllTimeProfit());
            QueryList.Add(new AllProductCountProfitQuery());
        }

        private void NewTablePage(object tableName)
        {
            switch (tableName.ToString())
            {
                case "ProductTable":
                    CurrentPage = _productPage;
                    break;
                case "ClientTable":
                    CurrentPage = _clientPage;
                    break;
                case "SellTable":
                    CurrentPage = _sellPage;
                    break;
                case "AvailableTable":
                    CurrentPage = _availabilityPage;
                    break;
                case "DeliverTable":
                    CurrentPage = _deliverPage;
                    break;
                case "CardTable":
                    CurrentPage = _cardPage;
                    break;
                case "DeliverProductTable":
                    CurrentPage = _deliverProductPage;
                    break;
                case "ProfileTable":
                    CurrentPage = _profilePage;
                    break;
            }
        }
    }
}
