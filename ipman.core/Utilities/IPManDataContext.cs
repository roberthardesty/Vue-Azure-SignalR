using ipman.shared.Entity;
using ipman.shared.Entity.Join;
using ipman.shared.Entity.Lookups;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ipman.core.Utilities
{
    public class IPManDataContext: DbContext
    {
        private const string ConnectionString = "SQLConnectionString";
        private IConfiguration _configuration;
        public IPManDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<SiteAccount> SiteAccounts { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteAccount>(site =>
            {
                site.HasKey(s => s.ID);
                site.HasData(SeedData.Sites);
            });
            modelBuilder.Entity<UserAccount>(user =>
            {
                user.HasData(SeedData.Users);
            });

            modelBuilder.Entity<Department>(dept =>
            {
                dept.HasData(SeedData.Departments);
            });

            modelBuilder.Entity<PostTag>()
            .HasKey(x => new { x.PostID, x.TagID });

            modelBuilder.Entity<SiteAccountUserAccount>(saua =>
            {
                saua.HasKey(x => new { x.SiteAccountID, x.UserAccountID });
                saua.HasData(SeedData.SiteUsers);
            });

            modelBuilder.Entity<SiteAccountUserAccountDepartment>(sauad =>
            {
                sauad.HasKey(x => new { x.SiteAccountUserAccountID, x.DepartmentID });
                sauad.HasData(SeedData.SiteUserDepartments);
            });


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration[ConnectionString]);
        }
    }
}
