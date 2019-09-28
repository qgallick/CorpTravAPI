using System;
using System.Collections.Generic;

namespace CorpTravAPI.Models
{
    public partial class User
    {
        public User()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
