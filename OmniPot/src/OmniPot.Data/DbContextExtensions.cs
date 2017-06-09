using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OmniPot.Data
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }
    }

    //NOTE: Rowversion in EF7 for timestamp doesn't apply to the database correctly? 
    //SEE: http://stackoverflow.com/questions/33970569/best-way-to-handle-optimistic-concurrency-with-entity-framework-7
    public static class SqlServerModelBuilderExtensions
    {
        public static PropertyBuilder<byte[]> IsRowVersion(this PropertyBuilder<byte[]> builder)
        {
            return builder.HasColumnType("rowversion").IsConcurrencyToken().ValueGeneratedOnAdd();
        }
    }
}
