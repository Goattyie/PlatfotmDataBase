using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Database.VeiwModel.Pages
{
    class ProfileViewModel: BasePropertyChanged
    {
        private Profile _profile;
        private BaseCommand _addCommand;

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
            get { return _addCommand ??= new BaseCommand(obj => 
            {
                try
                {
                    Service.profileMapper.Create(_profile);
                    _profile.Id = 0;
                    Service.profileMapper.NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Ошибка! В записи есть ошибки либо она уже существует.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }); }
        }
        public ProfileViewModel()
        {
            _profile = new Profile();
        }
    }
}
