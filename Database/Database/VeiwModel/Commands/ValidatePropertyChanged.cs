using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.VeiwModel.Commands
{
    abstract class ValidatePropertyChanged : BasePropertyChanged, IDataErrorInfo
    {
        public bool IsValid { get; set; }
        protected abstract Dictionary<string, string> _errors { get; set; }
        public string this[string columnName] => _errors.ContainsKey(columnName) ? _errors[columnName] : null;

        public string Error => throw new NotImplementedException();
        protected void UpdateIsValid()
        {
            IsValid = !_errors.Values.Any(x => x != null);
            OnPropertyChanged(nameof(IsValid));
        }
    }
}
