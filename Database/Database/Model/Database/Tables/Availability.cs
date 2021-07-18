using Database.Model.Database.ExcelWorkers;
using Database.Model.Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    /// <summary>
    /// Класс Наличие
    /// </summary>
    class Availability
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProfileId { get; set; }
        /// <summary>
        /// Количество товара
        /// </summary>
        /// <remarks>
        /// Не должно быть меньше нуля
        /// </remarks>
        public int Count { get; set; }
        public double BuyCost { get; set; }
        public double SellCost { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }

    }
}
