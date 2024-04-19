using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Models;

namespace WebPortal.DataAccessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProgramDetail> ProgramDetails { get; set; }

        public DbSet<Classes> Classes { get; set; }

        public DbSet<UserClasses> UserClasses { get; set; }

        public DbSet<UserProgramDetail> UserProgramDetails { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers {  get; set; }
    }
}
