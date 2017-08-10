using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class Tintuc
    {
        public int Matintuc { get; set; }
        public string Anh { get; set; }
        public int Machuyenmuc { get; set; }
        public DateTime? Ngaydang { get; set; }
        public string Noidung { get; set; }
        public string Tacgia { get; set; }
        public string Tieude { get; set; }
        public string Tieudecon { get; set; }

        public virtual Chuyenmuc MachuyenmucNavigation { get; set; }
    }
}
