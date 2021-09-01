using Database.VeiwModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Query
{
    abstract class  Query
    {
        public abstract string Name { get; set; }
        public BaseCommand ExecuteQueryCommand 
        { 
            get
            {
                return new BaseCommand(obj => { Algorithm(); });
            }
        }
        protected abstract void Algorithm();
    }
}
