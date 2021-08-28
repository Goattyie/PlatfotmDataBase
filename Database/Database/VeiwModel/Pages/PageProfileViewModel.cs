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
    class PageProfileViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Profile _selectedProfile;
        public Profile SelectedProfile
        {
            get { return _selectedProfile; }
            set { _selectedProfile = value; OnPropertyChanged(nameof(SelectedProfile)); }
        }
        public ObservableCollection<Profile> ProfileList { get; set; }
        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup("Profile").Show(); }));
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
                    Service.profileMapper.Delete(list.ToArray());
                }));
            }
        }
        public PageProfileViewModel()
        {
            ProfileList = new ObservableCollection<Profile>();
            Service.profileMapper.AddObserver(this);
            Execute();
        }
        public async void Execute()
        {
            ProfileList.Clear();
            var profiles = await Service.profileMapper.GetAllAsync();
            foreach (var item in profiles)
            {
                ProfileList.Add(item);
            }
        }
    }
}
