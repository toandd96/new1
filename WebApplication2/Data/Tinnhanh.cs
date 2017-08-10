using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class Tinnhanh
    {
        public int Matinnhanh { get; set; }
        public int Machuyenmuc { get; set; }
        public string Noidung { get; set; }

        public virtual Chuyenmuc MachuyenmucNavigation { get; set; }
    }
}
