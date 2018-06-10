using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DCNE_Cake.Models
{
    public class AppContext : DbContext
    {
        public AppContext()
            : base("DCNEConnection")
        {

        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<ComplaintCategory> ComplaintCates { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCates { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Removes the "s" suffix in table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}