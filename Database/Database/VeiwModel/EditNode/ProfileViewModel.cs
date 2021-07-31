using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Pages
{
    class ProfileViewModel: BasePropertyChanged
    {
        private Profile _profile;
        private BaseCommand _addCommand;
        private ProfileMapper _service;

        #region Поля
        public string Name
        {
            get { return _profile.Name; }
            set
            {
                _profile.Name = value; OnPropertyChanged(nameof(_profile.Name));
            }
        }
        #endregion
        public BaseCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new BaseCommand(obj => { _service.Create(_profile); _profile.Id = 0; })); }
        }
        public ProfileViewModel(ProfileMapper service)
        {
            _profile = new Profile();
            _service = service;
        }
    }
}
