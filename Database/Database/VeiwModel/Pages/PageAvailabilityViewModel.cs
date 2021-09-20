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
    class PageAvailabilityViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _updateCommand;
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _editCommand;
        private Availability _selectedAvailability;

        private double _deliverSum;
        private int _availabilityCount;

        public double DeliverSum
        {
            get { return _deliverSum; }
            set { _deliverSum = value; OnPropertyChanged(nameof(DeliverSum)); }
        }

        public int AvailabilityCount
        {
            get { return _availabilityCount; }
            set { _availabilityCount = value; OnPropertyChanged(nameof(AvailabilityCount)); }
        }

        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set { _selectedAvailability = value; OnPropertyChanged(nameof(SelectedAvailability)); }
        }
        public ObservableCollection<Availability> AvailabilityList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditAvailability().Show(); Execute(); }));
            }
        }
        public BaseCommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new BaseCommand(obj => { Execute(); }));
            }
        }
        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Availability>();
                    while (SelectedAvailability != null)
                    {
                        list.Add(_selectedAvailability);
                        AvailabilityList.Remove(_selectedAvailability);
                    }
                    Service.availabilityMapper.Delete(list.ToArray());
                }));
            }
        }
        public BaseCommand EditCommand
        {
            get
            {
                return _editCommand ??
                      (_editCommand = new BaseCommand(obj =>
                  {
                      if (_selectedAvailability is not null)
                          new EditAvailability(_selectedAvailability).Show();
                  }));
            }
        }

        public PageAvailabilityViewModel()
        {
            AvailabilityList = new ObservableCollection<Availability>();
            Service.productMapper.AddObserver(this);
            Service.availabilityMapper.AddObserver(this);
            Service.sellMapper.AddObserver(this);
            Service.orderNodeMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            DeliverSum = 0;
            AvailabilityCount = 0;
            AvailabilityList.Clear();
            var availability = await new AvailabilityMapper().GetAllAsync();
            availability = availability.OrderBy(x => x.Product.Name);
            foreach (var item in availability)
            {
                if (item.Count > 0)
                {
                    DeliverSum += item.DeliverCost * item.Count;
                    AvailabilityCount += item.Count;
                }
                AvailabilityList.Add(item);
            }
        }
    }
}
