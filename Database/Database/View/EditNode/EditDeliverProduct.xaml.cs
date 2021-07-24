﻿using Database.Model.Database.Services;
using Database.VeiwModel.EditNode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database.View.EditNode
{
    /// <summary>
    /// Логика взаимодействия для EditDeliverProduct.xaml
    /// </summary>
    public partial class EditDeliverProduct : Window
    {
        public EditDeliverProduct(DeliverProductMapper service)
        {
            InitializeComponent();
            DataContext = new DeliverProductViewModel(service);
        }
    }
}