using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SellCost { get; set; }
        public double OrderCost { get; set; }
        public double DeliverCost { get; set; }
        public double Profit { get; set; }
        public virtual ICollection<OrderNode> Orders { get; set; }
        public virtual ICollection<DeliverProduct> DeliverProducts { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
        public Product() { }
        public Product(string name)
        {
            Name = name;
        }
    }
}
