using DoctorAi.Domain._01.Entities._02.Verifycode;
using DoctorAi.Domain._01.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAi.Persistance._01.Contexts
{
    public class DoctorAiDbContext  : IdentityDbContext<UserApp, RoleApp, Guid>
    {
        public DoctorAiDbContext(DbContextOptions<DoctorAiDbContext> options):base (options)
        {
            

        }

        public DbSet<Token> Tokens { get; set; }
        public DbSet<VerifyCode> VerifyCodes { get; set; }

 
        protected override void OnModelCreating( ModelBuilder builder )
        {
            builder.Entity<UserApp>().HasOne(app => app.Token)
                .WithOne(T => T.User);
                 

            base.OnModelCreating(builder);
        }

    }
}
