using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class DriveType
    {
        public DriveType()
        {
            Car = new HashSet<Car>();
        }

        public int IdDriveType { get; set; }
        public string DriveTypeName { get; set; }

        public virtual ICollection<Car> Car { get; set; }
    }
}
