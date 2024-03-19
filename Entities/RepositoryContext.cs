using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext :DbContext
    {
        public RepositoryContext(DbContextOptions options) 
            : base(options) 
        { 
        }

        public DbSet<Owner>? Owners { get; set; }
        public DbSet<Account>? Accounts { get; set; }
        public DbSet<UserInfo>? UserInfos { get; set; }



    }
}
