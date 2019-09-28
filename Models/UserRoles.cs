using System;
using System.Collections.Generic;

namespace CorpTravAPI.Models
{
    public partial class UserRoles
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
