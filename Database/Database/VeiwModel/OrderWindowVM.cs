using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.ExcelParser;
using Database.Services.Interfaces;
using Database.VeiwModel.Commands;
using Database.View;
using Database.View.EditNode;
using Database.View.Pages.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Database.VeiwModel
{
    class OrderWindowVM:BasePropertyChanged, IObserver
    {
        private BaseCommand _addOrderCommand;
        private BaseCommand _addOrderToArchiveCommand;
        private BaseCommand _addCommand;
        private BaseCommand _editCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _acceptCommand;
        private BaseCommand _removeOrderCommand;
        private BaseCommand _excelExportCommand;
        private OrderNode _selecterOrderNode;
        private Model.Order _selectedOrder;
        private int _lastSelectedOrderId;
        public bool _isActive;

        public bool isActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(nameof(isActive)); }
        }
        public ObservableCollection<Model.Order> OrderList { get; set; }
        public Model.Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { 
                _selectedOrder = value;
                _lastSelectedOrderId = _selectedOrder?.Id ?? _lastSelectedOrderId;
                OnPropertyChanged(nameof(SelectedOrder));
                LoadOrderNodes();
            }
        }
        public BaseCommand AddOrderCommand
        {
            get { return _addOrderCommand ??= new BaseCommand(obj => 
            {
                try
                {
                    Service.orderMapper.Create(new Model.Order() { Date = DateTime.Now, IsActive = true });;
                    Service.orderMapper.NotifyObserver();
                }
                catch
                {

                }
            }); }
        }
        public BaseCommand AddOrderToArchiveCommand
        {
            get { return _addOrderToArchiveCommand ??= new BaseCommand(obj => 
            { 
                if(SelectedOrder != null)
                {
                    SelectedOrder.IsActive = false;
                    Service.orderMapper.Update(SelectedOrder);
                    Service.orderMapper.NotifyObserver();
                }
            }); }
        }
        public OrderNode SelectedOrderNode
        {
            get { return _selecterOrderNode; }
            set { _selecterOrderNode = value; OnPropertyChanged(nameof(SelectedOrderNode)); }
        }
        public ObservableCollection<OrderNode> OrderNodesList { get; set; }
        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditOrderNode(SelectedOrder).Show(); }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new BaseCommand(obj =>
                {
                    if (_selecterOrderNode != null)
                        new EditOrderNode(_selecterOrderNode).Show();
                }));
            }
        }
        public BaseCommand RemoveOrderCommand
        {
            get { return _removeOrderCommand ??= new BaseCommand((obj)=> 
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить заказ из базы данных?", "Уведомление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if(result == MessageBoxResult.OK)
                {
                    try
                    {
                        Service.orderMapper.Delete(SelectedOrder);
                        Execute();
                        if(OrderList.Count == 0)
                            OrderNodesList.Clear();
                    }
                    catch
                    {
                        MessageBox.Show("При удалении произошла ошибка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }); }
        }
        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<OrderNode>();
                    while (SelectedOrderNode != null)
                    {
                        list.Add(_selecterOrderNode);
                        OrderNodesList.Remove(_selecterOrderNode);
                    }
                    Service.orderNodeMapper.Delete(list.ToArray());
                    SelectedOrder = Service.orderMapper.GetAll().ToList().Find(x => x.Id == SelectedOrder.Id);
                    Service.orderMapper.ReCalculate(SelectedOrder);
                    Execute();
                }));
            }
        }
        public BaseCommand AcceptCommand
        {
            get { return _acceptCommand ?? (_acceptCommand = new BaseCommand(obj => { if (_selecterOrderNode != null) new AcceptOrder(_selecterOrderNode).ShowDialog(); })); }
        }
        public BaseCommand ExcelExportCommand
        {
            get { return _excelExportCommand ??= new BaseCommand((obj => { ExportToExcel(); })); }
        }
        public OrderWindowVM(bool isActive)
        {
            this._isActive = isActive;
            Service.orderMapper.AddObserver(this);
            Service.orderNodeMapper.AddObserver(this);
            OrderNodesList = new ObservableCollection<OrderNode>();
            OrderList = new ObservableCollection<Model.Order>();
            Execute();
        }
        public async void LoadOrderNodes()
        {
            if(SelectedOrder != null)
            {
                OrderNodesList.Clear();
                var list = await Service.orderNodeMapper.GetAllAsync();
                list = list.Where(x => x.OrderId == SelectedOrder.Id).ToList();
                foreach(var item in list)
                {
                    OrderNodesList.Add(item);
                }
            }
        }
        public void Execute()
        {
            OrderList.Clear();
            var orders = Service.orderMapper.GetAll().ToList().Where(x=>x.IsActive == _isActive);
            orders = orders.OrderByDescending(x => x.Id);
            foreach (var item in orders)
                OrderList.Add(item);
            SelectedOrder = OrderList.FirstOrDefault(x => x.Id == _lastSelectedOrderId);
            if(SelectedOrder == null)
                SelectedOrder = OrderList.FirstOrDefault();
        }

        private void ExportToExcel()
        {
            try
            {
                string dir = $"{Directory.GetCurrentDirectory()}\\Заказы";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                 new ExcelWriter(new OrderWriterStrategy(SelectedOrder), dir + $"\\Заказ№{SelectedOrder.Id}({SelectedOrder.Date.ToString("d")}).xlsx").WriteNodes();
            }
            catch
            {
                MessageBox.Show("При записи в файл возникла ошибка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
    }
}
