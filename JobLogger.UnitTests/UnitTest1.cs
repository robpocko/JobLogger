using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        private static bool setupHasBeenRun = false;

        [SetUp]
        public void Setup()
        {
            if (!setupHasBeenRun)
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
                setupHasBeenRun = true;
            }
        }

        [Test]
        public void Test1()
        {
            Feature feature = new Feature { ID = 1, Title = "Feature Number 1", Status = RequirementStatus.Active };
            feature = new FeatureBF(new JobLoggerDbContext()).Create(feature);

            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            Requirement requirement = new Requirement { ID = 1, Title = "Requirement Number 1", Status = RequirementStatus.Active };
            requirement = new RequirementBF(new JobLoggerDbContext()).Create(requirement);
        }

        [Test]
        public void Test3()
        {
            Task task = new Task { ID = 1, Title = "Task Number 1", TaskType = TaskType.Task, IsActive = true };
            task = new TaskBF(new JobLoggerDbContext()).Create(task);

            Assert.Pass();
        }

        [Test]
        public void Test4()
        {
            Feature feature =
                new Feature
                {
                    ID = 2,
                    Title = "Feature Number 2",
                    Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement { ID = 2, Title = "Requirement Number 2", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 2, Title = "Task Number 2", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 3, Title = "Task Number 3", TaskType = TaskType.Task, IsActive = true },
                                new Task { ID = 4, Title = "Bug Number 4", TaskType = TaskType.Bug, IsActive = true }
                            }
                        },
                        new Requirement { ID = 3, Title = "Requirement Number 3", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 5, Title = "Task Number 5", TaskType = TaskType.Task, IsActive = false },
                                new Task { ID = 6, Title = "Task Number 6", TaskType = TaskType.Task, IsActive = true },
                                new Task { ID = 7, Title = "Bug Number 7", TaskType = TaskType.Bug, IsActive = true }
                            }
                        }
                    }
                };
            feature = new FeatureBF(new JobLoggerDbContext()).Create(feature);

            Assert.Pass();
        }

        [Test]
        public void Test5()
        {
            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                int featureCount = db.Features.ToArray().Count();
                int requirementCount = db.Requirements.ToArray().Count();
                int taskCount = db.Tasks.ToArray().Count();
                int taskLogCount = db.TaskLogs.ToArray().Count();
                int checkInCount = db.CheckIns.ToArray().Count();
                int taskCheckInCount = db.TaskCheckIns.ToArray().Count();

                Feature feature = new Feature
                {
                    ID = 3, Title = "Feature Number 3", Status = RequirementStatus.Active,
                    Requirements = new List<Requirement>
                    {
                        new Requirement
                        {
                            ID = 4, Title = "Requirement Number 4", Status = RequirementStatus.Active,
                            Tasks = new List<Task>
                            {
                                new Task { ID = 8, Title = "Task Number 8", TaskType = TaskType.Task, IsActive = true }
                            }
                        }
                    }
                };
                feature = new FeatureBF(db).Create(feature);
                Assert.AreEqual(featureCount + 1, db.Features.ToArray().Count());
                Assert.AreEqual(requirementCount + 1, db.Requirements.ToArray().Count());
                Assert.AreEqual(taskCount + 1, db.Tasks.ToArray().Count());
                Assert.AreEqual(taskLogCount, db.TaskLogs.ToArray().Count());
                Assert.AreEqual(checkInCount, db.CheckIns.ToArray().Count());
                Assert.AreEqual(taskCheckInCount, db.TaskCheckIns.ToArray().Count());

                TaskLog newTaskLog = new TaskLog
                {
                    LogDate = DateTime.Parse("1-July-2019"),
                    StartTime = TimeSpan.Parse("08:30"),
                    EndTime = TimeSpan.Parse("10:15"),
                    Description = "TaskLog Number 1",
                    TaskID = 8
                };

                newTaskLog = new TaskLogBF(db).Create(newTaskLog);
                Assert.AreEqual(featureCount + 1, db.Features.ToArray().Count());
                Assert.AreEqual(requirementCount + 1, db.Requirements.ToArray().Count());
                Assert.AreEqual(taskCount + 1, db.Tasks.ToArray().Count());
                Assert.AreEqual(taskLogCount + 1, db.TaskLogs.ToArray().Count());
                Assert.AreEqual(checkInCount, db.CheckIns.ToArray().Count());
                Assert.AreEqual(taskCheckInCount, db.TaskCheckIns.ToArray().Count());

                CheckIn newCheckIn = new CheckIn
                {
                    ID = 1,
                    CheckInTime = DateTime.Parse("1-July-2019 09:43"),
                    Comment = "CheckIn Number 1",
                    CodeBranchID = db.CodeBranches.Where(c => c.Name == "Code Branch 1").Single().ID,
                    TaskCheckIns = new List<TaskCheckIn> { new TaskCheckIn { TaskID = 8 } },
                    TaskLog = newTaskLog
                };
                newCheckIn = new CheckInBF(db).Create(newCheckIn);
                Assert.AreEqual(featureCount + 1, db.Features.ToArray().Count());
                Assert.AreEqual(requirementCount + 1, db.Requirements.ToArray().Count());
                Assert.AreEqual(taskCount + 1, db.Tasks.ToArray().Count());
                Assert.AreEqual(taskLogCount + 1, db.TaskLogs.ToArray().Count());
                Assert.AreEqual(checkInCount + 1, db.CheckIns.ToArray().Count());
                Assert.AreEqual(taskCheckInCount + 1, db.TaskCheckIns.ToArray().Count());

                Task fetchedTask = db.Tasks
                                    .Where(t => t.ID == 8)
                                    .Include(l => l.Logs)
                                    .ThenInclude(c => c.CheckIns)
                                    .ThenInclude(t => t.TaskCheckIns)
                                    .Single();

                Assert.AreEqual(1, fetchedTask.Logs.Count());

            }
        }
    }
}