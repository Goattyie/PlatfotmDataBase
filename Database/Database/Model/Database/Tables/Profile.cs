using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Availability> Availabilities { get; set; }
    }
}
