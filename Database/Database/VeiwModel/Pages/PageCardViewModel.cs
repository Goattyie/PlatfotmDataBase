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
    class PageCardViewModel: BasePropertyChanged, IObserver
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Card _selectedCard;

        public Card SelectedCard
        {
            get { return _selectedCard; }
            set { _selectedCard = value; OnPropertyChanged(nameof(SelectedCard)); }
        }
        public BindingList<Card> CardList { get; set; }

        public BaseCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup("Card").Show(); Execute(); }));
            }
        }

        public BaseCommand RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = new BaseCommand(obj => {
                    var list = new List<Card>();
                    while (SelectedCard != null)
                    {
                        list.Add(_selectedCard);
                        CardList.Remove(_selectedCard);
                    }
                    Service.cardMapper.Delete(list.ToArray());
                }));
            }
        }

        public PageCardViewModel()
        {
            CardList = new BindingList<Card>();
            Service.cardMapper.AddObserver(this);
            Execute();
        }


        public async void Execute()
        {
            CardList.Clear();
            var card = await new CardMapper().GetAllAsync();
            card = card.OrderBy(x => x.Name);
            foreach (var item in card)
            {
                CardList.Add(item);
            }
        }
    }
}
