using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Robust.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robust.Context
{
    public class RobustContext:IdentityDbContext<User>
    {
       public DbSet<Product> Products { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Order> Orders { get; set; }
       public DbSet<OrderItem> OrderItems { get; set; }

        public RobustContext(DbContextOptions<RobustContext> options)
             : base(options)
        {

        }
        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<BaseEntity<Guid>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.UtcNow;
                    
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedDate = DateTime.UtcNow;
                    
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity<Guid>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedDate = DateTime.Now;
                   
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedDate = DateTime.Now;
                    
                }
            }
            return base.SaveChangesAsync();
        }
    }
}
