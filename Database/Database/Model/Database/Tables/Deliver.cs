using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    public class Deliver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DeliverProduct> DeliverProducts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
