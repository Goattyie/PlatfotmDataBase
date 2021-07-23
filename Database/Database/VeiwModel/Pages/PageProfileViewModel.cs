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
    class PageProfileViewModel:BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Profile _selectedProfile;
        private ProfileMapper _service;

        public Profile SelectedProfile
        {
            get { return _selectedProfile; }
            set { _selectedProfile = value; OnPropertyChanged(nameof(SelectedProfile)); }
        }
        public BindingList<Profile> ProfileList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup(_service).Show(); DownloadData(); }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Profile>();
                    while (SelectedProfile != null)
                    {
                        list.Add(_selectedProfile);
                        ProfileList.Remove(_selectedProfile);
                    }
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageProfileViewModel()
        {
            ProfileList = new BindingList<Profile>();
            _service = new ProfileMapper();
            _service.CreateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public void DownloadData()
        {
            ProfileList.Clear();
            var profiles = new ProfileMapper().GetAll();
            foreach (var item in profiles)
            {
                ProfileList.Add(item);
            }
        }
    }
}
