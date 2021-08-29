using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public double OrderSum { get; set; }
        public double DeliverSum { get; set; }
        public double AllSum { get; set; }
        public virtual ICollection<OrderNode> OrderNodes { get; set; }
    }
}
