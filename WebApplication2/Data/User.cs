using System;
using System.Collections.Generic;

namespace WebApplication2.Data
{
    public partial class User
    {
        public int Manguoidung { get; set; }
        public string Hoten { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public string Email { get; set; }
        public string Diachi { get; set; }
        public bool? IsSupper { get; set; }
        public string Groupid { get; set; }


        public virtual UserGroup Group { get; set; }
    }
}
