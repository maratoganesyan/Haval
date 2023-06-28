using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<Users>();
        }

        public int IdRole { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
