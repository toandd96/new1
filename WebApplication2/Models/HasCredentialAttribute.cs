using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.Models
{
    public class HasCredentialAttribute:AuthorizeAttribute
    {
        public string RoleId { get; set; }

        //public override void AuthorizeCore(HttpContext httpContext)
        //{
            
        //}
    }
}
