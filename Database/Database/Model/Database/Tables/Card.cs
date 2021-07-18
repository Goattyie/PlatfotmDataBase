using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    /// <summary>
    /// Класс Карта
    /// </summary>
    class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
    }
}
