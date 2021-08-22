using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    public class Client
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
    }
}
