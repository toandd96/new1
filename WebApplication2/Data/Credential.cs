using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class Credential
    {
        public string UserGroupId { get; set; }
        public string RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual UserGroup UserGroup { get; set; }
    }
}
