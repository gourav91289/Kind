using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmniPot.Data
{
    public class TemporaryDbContextFactory : IDbContextFactory<KindDbContext>
    {
        public KindDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<KindDbContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=OmniPot;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new KindDbContext(builder.Options, new SeedUserContext());
        }
    }
}
