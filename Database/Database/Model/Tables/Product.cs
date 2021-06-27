using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Tables
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SellCost { get; set; }
        public double OrderCost { get; set; }
        public double DeliverCost { get; set; }
    }
}
