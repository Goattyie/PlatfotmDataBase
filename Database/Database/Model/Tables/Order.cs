using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Tables
{
    class Order
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int Count { get; set; }
        public double OrderCost { get; set; }
        public double DeliverCost { get; set; }
        public double SummCost { get; set; }
        public double CurrentCost { get; set; }//сколько уже заплачено
        public int CurrentCount { get; set; }
    }
}
