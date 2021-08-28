using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.Services.Interfaces;
using Database.VeiwModel.Commands;
using Database.View.EditNode;
using System;
using System.Collections.Generic;
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

        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set { _selectedAvailability = value; OnPropertyChanged(nameof(SelectedAvailability)); }
        }
        public BindingList<Availability> AvailabilityList { get; set; }

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
            AvailabilityList = new BindingList<Availability>();
            Service.productMapper.AddObserver(this);
            Service.availabilityMapper.AddObserver(this);
            Service.sellMapper.AddObserver(this);
            Service.orderMapper.AddObserver(this);
            Execute();
        }

        public async void Execute()
        {
            AvailabilityList.Clear();
            var availability = await new AvailabilityMapper().GetAllAsync();
            availability = availability.OrderBy(x => x.Product.Name);
            foreach (var item in availability)
            {
                AvailabilityList.Add(item);
            }
        }
    }
}
