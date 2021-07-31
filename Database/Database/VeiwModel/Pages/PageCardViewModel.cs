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
    class PageCardViewModel: BasePropertyChanged
    {
        private BaseCommand _addCommand;
        private BaseCommand _removeCommand;
        private Card _selectedCard;
        private CardMapper _service;

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
                return _addCommand ?? (_addCommand = new BaseCommand(obj => { new EditSup(_service).Show(); DownloadData(); }));
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
                    _service.Delete(list.ToArray());
                }));
            }
        }

        public PageCardViewModel()
        {
            CardList = new BindingList<Card>();
            _service = new CardMapper();
            _service.CreateEntityEvent += OnUpdate;
            DownloadData();
        }

        private void OnUpdate(object obj)
        {
            DownloadData();
        }

        public async void DownloadData()
        {
            CardList.Clear();
            var profiles = await new CardMapper().GetAllAsync();
            foreach (var item in profiles)
            {
                CardList.Add(item);
            }
        }
    }
}
