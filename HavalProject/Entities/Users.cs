using System;
using System.Collections.Generic;

namespace HavalProject.Entities
{
    public partial class Users
    {
        public Users()
        {
            OrderIdClientNavigation = new HashSet<Order>();
            OrderIdManagerNavigation = new HashSet<Order>();
        }

        public int IdUser { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IdRole { get; set; }
        public int IdGender { get; set; }
        public byte[] Photo { get; set; }

        public virtual Gender IdGenderNavigation { get; set; }
        public virtual Role IdRoleNavigation { get; set; }
        public virtual ICollection<Order> OrderIdClientNavigation { get; set; }
        public virtual ICollection<Order> OrderIdManagerNavigation { get; set; }
    }
}
