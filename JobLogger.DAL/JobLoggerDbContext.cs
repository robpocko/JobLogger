using Microsoft.EntityFrameworkCore;

namespace JobLogger.DAL
{
    public class JobLoggerDbContext : DbContext
    {
        public DbSet<Feature>               Features { get; set; }
        public DbSet<Requirement>           Requirements { get; set; }
        public DbSet<Task>                  Tasks { get; set; }
        public DbSet<TaskLog>               TaskLogs { get; set; }
        public DbSet<CheckIn>               CheckIns { get; set; }
        public DbSet<TaskCheckIn>           TaskCheckIns { get; set; }
        public DbSet<CodeBranch>            CodeBranches { get; set; }
        public DbSet<TaskLogComment>        TaskLogComments { get; set; }
        public DbSet<TaskComment>           TaskComments { get; set; }
        public DbSet<RequirementComment>    RequirementComments { get; set; }
        public DbSet<TimeLine>              TimeLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=RLP_JobLog;integrated security=SSPI");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskCheckIn>()
                .HasKey(t => new { t.TaskID, t.CheckInID });

            modelBuilder.Entity<CodeBranch>()
                .Property(b => b.IsActive)
                .HasDefaultValue(true);
        }
    }
}
