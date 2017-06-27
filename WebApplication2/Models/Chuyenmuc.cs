using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public partial class Chuyenmuc
    {
        public Chuyenmuc()
        {
            Tinnhanh = new HashSet<Tinnhanh>();
            Tintuc = new HashSet<Tintuc>();
        }

        public int Machuyenmuc { get; set; }
        [Required, Display(Name = "Tên chuyên mục")]
        public string Tenchuyenmuc { get; set; }
        [Required, Display(Name = "Số bài viết")]
        public int? Sobaiviet { get; set; }

        public virtual ICollection<Tinnhanh> Tinnhanh { get; set; }
        public virtual ICollection<Tintuc> Tintuc { get; set; }
    }
}
