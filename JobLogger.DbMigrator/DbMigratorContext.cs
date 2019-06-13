using JobLogger.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobLogger.DbMigrator
{
    public class DbMigratorContext : JobLoggerDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
