
using Database.Model.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    /// <summary>
    /// <param name="Availability">Товары в наличии</param>
    /// </summary>
    public class Availability
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? ProfileId { get; set; }
        public int Count { get; set; }
        public double BuyCost { get; set; }
        public double DeliverCost { get; set; }
        public double SellCost { get; set; }
        public string Comment { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Product Product { get; set; }

    }
}
