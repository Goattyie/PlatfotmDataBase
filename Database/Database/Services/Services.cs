using Database.Model.Database.Services;
using Database.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services
{
    static class Service
    {
        public static AvailabilityMapper availabilityMapper = new AvailabilityMapper();
        public static CardMapper cardMapper = new CardMapper();
        public static ClientMapper clientMapper = new ClientMapper();
        public static DeliverMapper deliverMapper = new DeliverMapper();
        public static DeliverProductMapper deliverProductMapper = new DeliverProductMapper();
        public static OrderMapper orderMapper = new OrderMapper();
        public static ProductMapper productMapper = new ProductMapper();
        public static ProfileMapper profileMapper = new ProfileMapper();
        public static SellMapper sellMapper = new SellMapper();
    }
}
