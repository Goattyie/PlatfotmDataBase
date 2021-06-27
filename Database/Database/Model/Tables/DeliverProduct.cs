using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Tables
{
    class DeliverProduct
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdDeliver { get; set; }
    }
}
