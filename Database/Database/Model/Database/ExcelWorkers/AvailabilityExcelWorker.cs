using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.ExcelWorkers
{
    class AvailabilityExcelWorker : IExcelWorker<Availability>
    {
        public IEnumerable<Availability> Read(string filename)
        {
            throw new NotImplementedException();
        }

        public void Write(IEnumerable<Availability> obj)
        {
            throw new NotImplementedException();
        }
    }
}
