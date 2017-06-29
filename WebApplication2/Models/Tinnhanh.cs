using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public partial class Tinnhanh
    {
        
        public int Matinnhanh { get; set; }
        [Required(ErrorMessage ="Chưa nhập nội dung bài viết"),Display(Name ="Nội dung của tin nhanh")]
        public string Noidung { get; set; }
        [Required(ErrorMessage ="Chưa nhập chuyên mục của bài viết"),Display(Name ="Chuyên mục của bài viết")]
        public int? Machuyenmuc { get; set; }
        [Display(Name ="Tên chuyên mục")]
        public virtual Chuyenmuc MachuyenmucNavigation { get; set; }
    }
}
