using Entities.Models;
using Entities.MongoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RepositoryMongoContext : DbContext
    {
        public RepositoryMongoContext(DbContextOptions<RepositoryMongoContext> options)
           : base(options)
        {
        }

        public DbSet<OwnerMongo>? Owners { get; set; }
    }
}
