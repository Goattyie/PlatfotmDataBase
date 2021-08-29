using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    class QueryListItem
    {
        private BaseCommand _executeCommand;
        public string Name { get; set; }
        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand; }
            set { _executeCommand = value; }
        }
    }
}
