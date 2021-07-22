﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Database.VeiwModel.Commands;
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
        private Page _orderPage;
        private Page _sellPage;
        private Page _profilePage;
        private Page _cardPage;

        private BaseCommand _tableClick;

        public BaseCommand TableClick
        {
            get
            {
                return _tableClick ??
                  (_tableClick = new BaseCommand((obj) => { NewTablePage(obj); }));
            }
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
            _orderPage = new Order();
            _profilePage = new Profile();
            _cardPage = new Card();

            _currentPage = null;
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
                case "OrderTable":
                    CurrentPage = _orderPage;
                    break;
                case "ProfileTable":
                    CurrentPage = _profilePage;
                    break;
            }
        }
    }
}
