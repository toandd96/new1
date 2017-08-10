using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class Chuyenmuc
    {
        public Chuyenmuc()
        {
            Tinnhanh = new HashSet<Tinnhanh>();
            Tintuc = new HashSet<Tintuc>();
        }

        public int Machuyenmuc { get; set; }
        public string Tenchuyenmuc { get; set; }

        public virtual ICollection<Tinnhanh> Tinnhanh { get; set; }
        public virtual ICollection<Tintuc> Tintuc { get; set; }
    }
}
