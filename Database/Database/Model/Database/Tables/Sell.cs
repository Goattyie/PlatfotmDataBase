using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    public class Sell
    {
        public int Id { get; set; }
        public string SellDate { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public double Profit { get; set; }
        public int ClientId { get; set; }
        public int CardId { get; set; }
        public Client Client { get; set; }
        public Card Card { get; set; }
        public Product Product { get; set; }
    }
}
