using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class CarPhoto
    {
        public int IdCar { get; set; }
        public int PhotoNumber { get; set; }
        public byte[] Photo { get; set; }

        public virtual Car IdCarNavigation { get; set; }
    }
}
