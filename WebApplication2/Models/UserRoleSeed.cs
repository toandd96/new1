using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class UserRoleSeed
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleSeed(RoleManager<IdentityRole> roleManage)
        {
            _roleManager = roleManage;
        }

        public async void Seed()
        {
            if ((await _roleManager.FindByNameAsync("User")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
            if ((await _roleManager.FindByNameAsync("Admin")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
            if ((await _roleManager.FindByNameAsync("Mod")) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            }
        }
    }
}
