using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Tables
{
    class Availability
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdProfile { get; set; }
        public double BuyCost { get; set; }
        public double SellCost { get; set; }
    }
}
