using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    class SellExcelWorker : IExcelWorker<Sell>
    {
        public IEnumerable<Sell> Read(string filename)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<Sell> obj)
        {
            throw new NotImplementedException();
        }
    }
}
