using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Gender
    {
        public Gender()
        {
            Users = new HashSet<Users>();
        }

        public int IdGender { get; set; }
        public string GenderName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
