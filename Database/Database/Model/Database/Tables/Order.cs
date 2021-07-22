using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Tables
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int DeliverId { get; set; }
        public int Count { get; set; }
        public double OrderCost { get; set; }
        public double DeliverCost { get; set; }
        public double SummCost { get; set; }
        /// <summary>
        /// Сколько заплачено на данный момент
        /// </summary>
        public double CurrentCost { get; set; }
        /// <summary>
        /// Сколько товара получено на данный момент
        /// </summary>
        public int CurrentCount { get; set; }
        public virtual Product Product { get; set; }
        public virtual Deliver Deliver { get; set; }
    }
}
