using ipman.shared.Entity;
using ipman.shared.Entity.Join;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteAccountUserAccount>()
            .HasKey(x => new { x.SiteAccountID, x.UserAccountID });

            modelBuilder.Entity<SiteAccountUserAccountDepartment>()
            .HasKey(x => new  { x.SiteAccountUserAccountID, x.DepartmentID } );

            modelBuilder.Entity<PostTag>()
            .HasKey(x => new { x.PostID, x.TagID });



            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration[ConnectionString]);
        }
    }
}
