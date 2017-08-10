using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class Role
    {
        public Role()
        {
            Credential = new HashSet<Credential>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Credential> Credential { get; set; }
    }
}
