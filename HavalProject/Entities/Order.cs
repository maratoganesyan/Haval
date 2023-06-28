using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Order
    {
        public Order()
        {
            ServicesInOrders = new HashSet<ServicesInOrders>();
        }

        public int IdOrder { get; set; }
        public int IdCar { get; set; }
        public int IdClient { get; set; }
        public int IdManager { get; set; }
        public DateTime DateTimeOfOrder { get; set; }
        public decimal? TotalPrice { get; set; }
        public int IdOrderStatus { get; set; }

        public virtual Car IdCarNavigation { get; set; }
        public virtual Users IdClientNavigation { get; set; }
        public virtual Users IdManagerNavigation { get; set; }
        public virtual OrderStatuses IdOrderStatusNavigation { get; set; }
        public virtual ICollection<ServicesInOrders> ServicesInOrders { get; set; }
    }
}
