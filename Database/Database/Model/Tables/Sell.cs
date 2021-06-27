using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Tables
{
    class Sell
    {
        public int Id { get; set; }
        public string SellDate { get; set; }
        public int IdAvailability { get; set; }
        public int Count { get; set; }
        public double Profit { get; set; }
        public int IdClient { get; set; }
        public int IdCard { get; set; }
    }
}
