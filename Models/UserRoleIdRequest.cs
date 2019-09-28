using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorpTravAPI.Models
{
    //DTO for getting userrole data for record creation from post body
    public class UserRoleIdRequest
    {
        public string UserId { get; set; }
        public int RoleId { get; set; }
    }
}
