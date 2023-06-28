using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class OrderStatuses
    {
        public OrderStatuses()
        {
            Order = new HashSet<Order>();
        }

        public int IdOrderStatus { get; set; }
        public string OrderStatusName { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
