using Microsoft.EntityFrameworkCore;
using RestApiLesson.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiLesson
{
    public class PostamatsContext : DbContext
    {
        public DbSet<Postamat> Postamats { get; set; }
        public DbSet<Order> Orders { get; set; }

        public PostamatsContext(DbContextOptions<PostamatsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .Property(or => or.Products)
                .HasConversion(
                v => string.Join(";", v), 
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray());

        }
    }
}
