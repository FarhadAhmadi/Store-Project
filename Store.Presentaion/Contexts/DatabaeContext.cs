using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Common;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentaion.Contexts
{
    public class DatabaeContext:DbContext,IDatabaseContext
    {
        public DatabaeContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Role> Roles{ get; set; } 
        public DbSet<UserInRole> UserInRoles { get; set; } 


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role { Id = 1, name = nameof(UserRoles.Admin) });
            builder.Entity<Role>().HasData(new Role { Id = 2, name = nameof(UserRoles.Operator) });
            builder.Entity<Role>().HasData(new Role { Id = 3, name = nameof(UserRoles.Customer) });
        }
    }
}
