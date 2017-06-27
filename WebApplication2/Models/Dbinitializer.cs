using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Dbinitializer
    {
        public static void Initialize(WebTTContext context)
        {
            context.Database.EnsureCreated();
            if(context.Tintuc==null)
            {
                return;
            }

        }
    }
}
