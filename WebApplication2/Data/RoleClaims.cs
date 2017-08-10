using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class RoleClaims
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string RoleId { get; set; }

        public virtual Roles Role { get; set; }
    }
}
