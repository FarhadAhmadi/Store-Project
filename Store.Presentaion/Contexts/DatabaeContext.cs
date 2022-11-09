using Microsoft.EntityFrameworkCore;
using Store.Aplication.Interface.Contexts;
using Store.Common;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Presentaion.Contexts
{
    public class DatabaeContext : DbContext, IDatabaseContext
    {
        public DatabaeContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<ProductFeaTures> ProductFeaTures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetDataForRoles(builder);
            SetFilter(builder);
            SetIndex(builder);
        }
        public void SetIndex(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(e => e.Email).IsUnique();
        }
        private void SetFilter(ModelBuilder builder)
        {
            builder.Entity<User>().HasQueryFilter(e => !e.IsRemoved);
        }
        private void SetDataForRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            builder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            builder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });
        }
    }
}