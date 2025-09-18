using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using payrallproject.Models.Domains;

namespace payrallproject.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Employe> Employe { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<PasswordResetTokens> PasswordResetTokens { get; set; }
        public DbSet<OT> OT { get; set; }
        public DbSet<EmployeeCategories> EmployeeCategories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeOvertime> EmployeeOvertimes { get; set; }
        public DbSet<SalaryReport> SalaryReports { get; set; }
        public DbSet<Loans> Loans { get; set; }
        public DbSet<LoanRepayment> Loanrepayment { get; set; }
        public DbSet<Leaves> Leaves { get; set; }
        public DbSet<NoPayDay> NoPayDay { get; set; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //var AdminRoleId = "e5a00264-e2a2-4c67-a568-70f48d9aa34f";
        //var roles = new List<IdentityRole>
        //{
        //    new IdentityRole
        //    {
        //        Id = AdminRoleId,
        //        ConcurrencyStamp = AdminRoleId,
        //        Name = "Admin",
        //        NormalizedName = "Admin".ToUpper(),
        //    }
        //};
        //builder.Entity<IdentityRole>().HasData(roles);

        //var adminUserId = "75af95a9-9273-4c9b-86aa-0a80c76f32d6";
        //var admin = new User()
        //{
        //    Id = adminUserId,
        //    UserName = "admin@arithmos.com",
        //    Email = "admin@arithmos.com",
        //    NormalizedEmail = "admin@arithmos.com".ToUpper(),
        //    NormalizedUserName = "admin@arithmos.com".ToUpper(),
        //};

        //admin.PasswordHash = new PasswordHasher<User>().HashPassword(admin, "Admin@12345");

        //builder.Entity<User>().HasData(admin);

        //var adminRole = new IdentityUserRole<string>()
        //{
        //    UserId = adminUserId,
        //    RoleId = AdminRoleId
        //};

        //builder.Entity<IdentityUserRole<string>>().HasData(adminRole);
        //}
    }
}
