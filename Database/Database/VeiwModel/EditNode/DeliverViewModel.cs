using Database.Model.Database.Services;
using Database.Model.Database.Tables;
using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.EditNode
{
    class DeliverViewModel: BasePropertyChanged
    {
        private Deliver _deliver;
        private BaseCommand _addCommand;
        private DeliverMapper _service;

        #region Поля
        public string Name
        {
            get { return _deliver.Name; }
            set
            {
                _deliver.Name = value; OnPropertyChanged(nameof(_deliver.Name));
            }
        }
        #endregion
        public BaseCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new BaseCommand(obj => { _service.Create(_deliver); _deliver.Id = 0; })); }
        }
        public DeliverViewModel(DeliverMapper service)
        {
            _deliver = new Deliver();
            _service = service;
        }
    }
}
