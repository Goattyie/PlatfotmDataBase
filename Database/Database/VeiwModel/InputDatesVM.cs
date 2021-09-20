using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel
{
    class InputDatesVM:BasePropertyChanged
    {
        private DateTime _firstDate;
        private DateTime _secondDate;
        private BaseCommand _executeCommand;

        public DateTime FirstDate
        {
            get { return _firstDate; }
            set { _firstDate = value; OnPropertyChanged(nameof(FirstDate)); }
        }

        public DateTime SecondDate
        {
            get { return _secondDate; }
            set { _secondDate = value; OnPropertyChanged(nameof(SecondDate)); }
        }

        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand; }
        }

        public InputDatesVM()
        {
            FirstDate = DateTime.Now;
            SecondDate = DateTime.Now;
        }
    }
}
