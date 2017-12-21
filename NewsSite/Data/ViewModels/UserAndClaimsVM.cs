using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewsSite.Data
{
    public class UserAndClaimsVM
    {
        public ApplicationUser User { get; set; }
        public List<Claim> Claims { get; set; }
    }
}
