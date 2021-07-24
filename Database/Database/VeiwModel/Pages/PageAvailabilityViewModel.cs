using Database.Model.Database.Services;
using Database.Model.Database.Tables;
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
    class PageAvailabilityViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private BaseCommand _editCommand;
        private Availability _selectedAvailability;
        private AvailabilityMapper _service;

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
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditAvailability(_service).Show(); DownloadData(); }));
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
                    _service.Delete(list.ToArray());
                }));
            }
        }
        public BaseCommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new BaseCommand(obj => { new EditAvailability(_service, _selectedAvailability).Show(); DownloadData(); })); }
        }

        public PageAvailabilityViewModel()
        {
            AvailabilityList = new BindingList<Availability>();
            _service = new AvailabilityMapper();
            SellMapper.CreateEntityEvent += OnUpdate;
            _service.CreateEntityEvent += OnUpdate;
            _service.UpdateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public void DownloadData()
        {
            AvailabilityList.Clear();
            var profiles = new AvailabilityMapper().GetAll();
            foreach (var item in profiles)
            {
                AvailabilityList.Add(item);
            }
        }
    }
}
