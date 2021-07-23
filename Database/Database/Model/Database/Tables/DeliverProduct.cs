using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    /// <summary>
    /// Класс Поставщики-Товары
    /// </summary>
    public class DeliverProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DeliverId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Deliver Deliver { get; set; }

    }
}
