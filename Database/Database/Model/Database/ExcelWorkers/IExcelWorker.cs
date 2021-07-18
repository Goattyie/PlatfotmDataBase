using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    interface IExcelWorker<T> where T:class
    {
        void Write(IEnumerable<T> obj);
        IEnumerable<T> Read(string filename);
    }
}
