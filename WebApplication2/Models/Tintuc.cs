using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public partial class Tintuc
    {
        public int Matintuc { get; set; }
        [Required,Display(Name ="Tiêu đề")]
        public string Tieude { get; set; }
        [Required, Display(Name = "Tiêu đề con")]
        public string Tieudecon { get; set; }
        [Required, Display(Name = "Nội dung")]
        public string Noidung { get; set; }
        //[Required(ErrorMessage ="Có đéo ảnh đâu"), Display(Name = "Ảnh")]
        public string Anh { get; set; }
        public DateTime? Ngaydang { get; set; }
        [Required, Display(Name = "Tác gải")]
        public string Tacgia { get; set; }
        [Required, Display(Name = "Mã chuyên mục")]
        public int? Machuyenmuc { get; set; }

        public virtual Chuyenmuc MachuyenmucNavigation { get; set; }
    }
}
