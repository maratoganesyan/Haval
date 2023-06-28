using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class AdditationalServices
    {
        public AdditationalServices()
        {
            ServicesInOrders = new HashSet<ServicesInOrders>();
        }

        public int IdService { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }

        public virtual ICollection<ServicesInOrders> ServicesInOrders { get; set; }
    }
}
