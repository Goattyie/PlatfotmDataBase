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

namespace Database.VeiwModel.EditNode
{
    class CardViewModel: BasePropertyChanged
    {
        private Card _card;
        private BaseCommand _addCommand;

        #region Поля
        public string Name
        {
            get { return _card.Name; }
            set
            {
                _card.Name = value; OnPropertyChanged(nameof(_card.Name));
            }
        }
        #endregion
        public BaseCommand AddCommand
        {
            get { return _addCommand ??= new BaseCommand(obj =>
            {
                try
                {
                    Service.cardMapper.Create(_card); 
                    _card.Id = 0;
                    Service.cardMapper.NotifyObserver();
                }
                catch
                {
                    MessageBox.Show("Ошибка! В записи есть ошибки либо она уже существует.", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }); }
        }
        public CardViewModel()
        {
            _card = new Card();
        }
    }
}
