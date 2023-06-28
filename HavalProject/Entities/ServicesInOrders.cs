using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class ServicesInOrders
    {
        public int IdOrder { get; set; }
        public int IdService { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual AdditationalServices IdServiceNavigation { get; set; }
    }
}
