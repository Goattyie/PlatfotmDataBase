using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Database.View.Pages;
using Database.View.Pages.Tables;

namespace Database.VeiwModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Page _currentPage;

        private Page _availabilityPage;
        private Page _clientPage;
        private Page _productPage;
        private Page _deliverPage;
        private Page _deliverProductPage;
        private Page _orderPage;
        private Page _sellPage;
        private Page _profilePage;
        private Page _cardPage;
        private Page _queryPage;

        private BaseCommand _tableClick;
        private BaseCommand _queryClick;

        public BaseCommand TableClick
        {
            get
            {
                return _tableClick ??
                  (_tableClick = new BaseCommand((obj) => { NewTablePage(obj); }));
            }
        }
        public BaseCommand QueryClick
        {
            get { return _queryClick ?? (_queryClick = new BaseCommand((obj) => { CurrentPage = _queryPage; })); }
        }
        public Page CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged(nameof(CurrentPage)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
        public MainViewModel()
        {
            _sellPage = new Sell();
            _availabilityPage = new Availability();
            _clientPage = new Client();
            _productPage = new Product();
            _deliverPage = new Deliver();
            _deliverProductPage = new DeliverProduct();
            _orderPage = new Order();
            _profilePage = new Profile();
            _cardPage = new Card();

            _queryPage = new Query();
            _currentPage = _sellPage;
        }
        public void NewTablePage(object tableName)
        {
            switch (tableName.ToString())
            {
                case "Товар":
                    CurrentPage = _productPage;
                    break;
                case "Клиент":
                    CurrentPage = _clientPage;
                    break;
                case "Продажа":
                    CurrentPage = _sellPage;
                    break;
                case "Наличие":
                    CurrentPage = _availabilityPage;
                    break;
                case "Поставщик":
                    CurrentPage = _deliverPage;
                    break;
                case "Карта":
                    CurrentPage = _cardPage;
                    break;
                case "Поставщик-Товар":
                    CurrentPage = _deliverProductPage;
                    break;
                case "Заказ":
                    CurrentPage = _orderPage;
                    break;
                case "Профиль":
                    CurrentPage = _profilePage;
                    break;
            }
        }
    }
}
