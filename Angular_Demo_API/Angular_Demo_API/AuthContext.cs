using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Angular_Demo_API
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext(): base("DefaultConnection")
        {

        }
        public static AuthContext Create()
        {
            return new AuthContext();
        }
    }
}