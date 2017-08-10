using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class UserGroup
    {
        public UserGroup()
        {
            Credential = new HashSet<Credential>();
            User = new HashSet<User>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Credential> Credential { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
