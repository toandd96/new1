using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class UserClaims
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
