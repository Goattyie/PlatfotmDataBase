using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.Services;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return _addCommand ?? (_addCommand = new BaseCommand(obj => { Service.cardMapper.Create(_card); _card.Id = 0; })); }
        }
        public CardViewModel()
        {
            _card = new Card();
        }
    }
}
