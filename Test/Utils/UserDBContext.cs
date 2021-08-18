using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Authentication;
using Test.Models;

namespace Test.Utils
{
    public class UserDBContext : IdentityDbContext<ApplicationUser>
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> users { get; set; }
    }
}
