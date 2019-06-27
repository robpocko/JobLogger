using JobLogger.BF;
using JobLogger.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobLogger.UnitTests
{
    public class GlobalCommon
    {
        public static bool dataHasBeenCleared = false;

        public static void ClearData()
        {
            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                db.Database.ExecuteSqlCommand("delete from CodeBranch");
                db.Database.ExecuteSqlCommand("delete from TaskLog");
                db.Database.ExecuteSqlCommand("delete from Task");
                db.Database.ExecuteSqlCommand("delete from Requirement");
                db.Database.ExecuteSqlCommand("delete from Feature");

                new CodeBranchBF(db).Create(new CodeBranch { Name = "Code Branch 1" });
            }
            dataHasBeenCleared = true;
        }
    }
}
