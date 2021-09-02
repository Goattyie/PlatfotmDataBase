using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Query
{
    public class ProductCountProfite
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public double Profit { get; set; }
        public double PersentCount { get; set; }
        public double PersentProfit { get; set; }
    }
}
