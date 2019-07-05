using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.UnitTests
{
    public class FeatureTests
    {
        [SetUp]
        public void Setup()
        {
            if (!GlobalCommon.dataHasBeenCleared)
            {
                GlobalCommon.ClearData();
            }
        }

        [Test]
        public void FeatureTest1()
        {
            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                FeatureAPI feature = GlobalCommon.NewFeature(5, "Feature Number 5");
                feature.Requirements.Add(GlobalCommon.NewRequirement(7, "Requirement Number 7"));
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment Number 1" });
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment Number 2" });
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(11, "Task Number 11"));
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment Number 1" });
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment Number 2" });
                feature.Requirements[0].Tasks[0].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("1 Jan 2019 09:00")));
                feature.Requirements[0].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "TaskLog Comment Number 1" });
                feature.Requirements[0].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "TaskLog Comment Number 2" });
                feature.Requirements.Add(GlobalCommon.NewRequirement(8, "Requirement Number 8"));

                Assert.IsTrue(feature.IsNew);
                Assert.IsTrue(feature.Requirements[0].IsNew);
                Assert.IsTrue(feature.Requirements[1].IsNew);
                Assert.IsTrue(feature.Requirements[0].Tasks[0].IsNew);

                feature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(feature)));
                Assert.IsFalse(feature.IsNew);
                Assert.IsFalse(feature.Requirements[0].IsNew);
                Assert.IsFalse(feature.Requirements[1].IsNew);
                Assert.IsFalse(feature.Requirements[0].Tasks[0].IsNew);

                FeatureAPI fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(5));

                CompareFeatureToFetchedFeature(feature, fetchedFeature);
            }
        }

        [Test]
        public void FeatureTest2()
        {
            FeatureAPI feature = GlobalCommon.NewFeature(6, "Feature Number 6");

            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                feature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(feature)));
                Assert.IsFalse(feature.IsNew);

                feature.Requirements.Add(GlobalCommon.NewRequirement(9, "Requirement Number 9"));
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 1 for Requirement 9" });
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 2 for Requirement 9" });
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(12, "Task Number 12"));
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment 1 for Task 12" });
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment 2 for Task 12" });
                feature.Requirements[0].Tasks[0].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("1 Jan 2019 09:00")));
                feature.Requirements[0].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 1 for Task 12" });
                feature.Requirements[0].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 1 for Task 12" });
                feature.Requirements[0].Tasks[0].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("1 Jan 2019 11:30")));
                feature.Requirements[0].Tasks[0].Logs[1].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 2 for Task 12" });
                feature.Requirements[0].Tasks[0].Logs[1].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 2 for Task 12" });
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(13, "Task Number 13"));
                feature.Requirements[0].Tasks[1].Comments.Add(new TaskCommentAPI { Comment = "Comment 1 for Task 13" });
                feature.Requirements[0].Tasks[1].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("1 Jan 2019 14:00")));
                feature.Requirements[0].Tasks[1].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Time Log for Task 13" });
                feature.Requirements[0].Tasks[1].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 2 for Time Log for Task 13" });
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(14, "Task Number 14"));
                feature.Requirements[0].Tasks[2].Comments.Add(new TaskCommentAPI { Comment = "Comment 1 for Task 14" });
                feature.Requirements[0].Tasks[2].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("2 Jan 2019 09:00")));
                feature.Requirements[0].Tasks[2].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Time Log for Task 14" });
                feature.Requirements[0].Tasks[2].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 2 for Time Log for Task 14" });
                feature.Requirements[0].Tasks[2].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("2 Jan 2019 11:00")));
                feature.Requirements[0].Tasks[2].Logs[1].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 2 for Task 14" });
                feature.Requirements[0].Tasks[2].Logs[1].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 2 for Task 14" });

                feature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(feature)));
                CheckObjectCount(feature, 1, 2, 3, 4, 5, 10, 0, 0);
                Assert.IsFalse(feature.Requirements[0].IsNew);

                feature.Requirements.Add(GlobalCommon.NewRequirement(10, "equirement Number 10"));
                feature.Requirements[1].Comments.Add(new RequirementCommentAPI { Comment = "Comment for Requirement Number 10" });
                feature.Requirements[1].Tasks.Add(GlobalCommon.NewTask(15, "Task Number 15"));
                feature.Requirements[1].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Task Comment for Task 15" });
                feature.Requirements[1].Tasks[0].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("3 Jan 2019 09:00")));
                feature.Requirements[1].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for Task 1 for Task Number 15" });

                feature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(feature)));

                FeatureAPI fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(feature, 2, 3, 4, 5, 6, 11, 0, 0);

                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 3 for Requirement 9" });
                feature.Requirements[0].Title = "Updated Requirement Number 9";
                feature.Requirements[0].Comments[0].Comment = "Updated Comment for Requirement Number 9";
                feature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(feature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(feature, 2, 4, 4, 5, 6, 11, 0, 0);
                Assert.AreEqual("Comment 3 for Requirement 9", fetchedFeature.Requirements[0].Comments[2].Comment);
                Assert.AreEqual("Updated Requirement Number 9", fetchedFeature.Requirements[0].Title);
                Assert.AreEqual("Updated Comment for Requirement Number 9", fetchedFeature.Requirements[0].Comments[0].Comment);

                feature.Requirements[1].Tasks.Add(new TaskAPI
                {
                    ID = 16,
                    Title = "Bug Number 16",
                    IsActive = true,
                    TaskType = TaskType.Bug,
                    IsNew = true
                });

                feature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(feature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(feature, 2, 4, 5, 5, 6, 11, 0, 0);
                CheckObjectCount(fetchedFeature, 2, 4, 5, 5, 6, 11, 0, 0);

                feature.Requirements[1].Tasks[1].Comments = new List<TaskCommentAPI>
                {
                    new TaskCommentAPI { Comment = "Comment 1 for Bug 16" },
                    new TaskCommentAPI { Comment = "Comment 2 for Bug 16" }
                };


                feature.Requirements[1].Tasks[1].Logs = new List<TaskLogAPI>
                {
                    new TaskLogAPI
                    {
                        LogDate = DateTime.Parse("2 Jan 2019"),
                        StartTime = TimeSpan.Parse("09:00"),
                        EndTime = TimeSpan.Parse("12:00"),
                        Description = "Task log 1 for Bug 16",
                        Comments = new List<TaskLogCommentAPI>
                        {
                            new TaskLogCommentAPI { Comment = "Comment for Log 1 of Bug 16" }
                        }
                    },
                    new TaskLogAPI
                    {
                        LogDate = DateTime.Parse("2 Jan 2019"),
                        StartTime = TimeSpan.Parse("13:00"),
                        EndTime = TimeSpan.Parse("15:00"),
                        Description = "Task log 2 for Bug 16",
                        Comments = new List<TaskLogCommentAPI>
                        {
                            new TaskLogCommentAPI { Comment = "Comment for Log 2 of Bug 16" }
                        },
                        CheckIns = new List<CheckInAPI>
                        {
                            new CheckInAPI
                            {
                                ID = 2,
                                CheckInTime = DateTime.Parse("2 Jan 2019 14:34"),
                                CodeBranchID = db.CodeBranches.Where(c => c.Name == "Code Branch 1").Single().ID,
                                Comment = "CheckIn Number 2",
                                IsNew = true,
                                TaskCheckIns = new List<TaskCheckInAPI>
                                {
                                    new TaskCheckInAPI { TaskID = feature.Requirements[1].Tasks[1].ID }
                                }
                            }
                        }
                    }
                };

                feature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(feature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(       feature, 2, 4, 5, 7, 8, 13, 1, 1);
                CheckObjectCount(fetchedFeature, 2, 4, 5, 7, 8, 13, 1, 1);
            }
        }

        [Test]
        public void FeatureTest3()
        {
            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                long codeBranchId = db.CodeBranches.First().ID;

                FeatureAPI feature = GlobalCommon.NewFeature(7, "Feature Number 7");
                feature.Requirements.Add(GlobalCommon.NewRequirement(11, "Requirement Number 11"));
                feature.Requirements.Add(GlobalCommon.NewRequirement(12, "Requirement Number 12"));
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(17, "Task Number 17"));
                feature.Requirements[0].Tasks.Add(GlobalCommon.NewTask(18, "Task Number 18"));
                feature.Requirements[1].Tasks.Add(GlobalCommon.NewTask(19, "Task Number 19"));
                feature.Requirements[1].Tasks.Add(GlobalCommon.NewTask(20, "Task Number 20"));
                feature.Requirements[1].Tasks.Add(GlobalCommon.NewTask(21, "Task Number 21"));
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 1 for Requirement 11" });
                feature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 2 for Requirement 11" });
                feature.Requirements[1].Comments.Add(new RequirementCommentAPI { Comment = "Comment 1 for Requirement 12" });
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment 1 for Task 11" });
                feature.Requirements[0].Tasks[0].Comments.Add(new TaskCommentAPI { Comment = "Comment 2 for Task 11" });
                feature.Requirements[0].Tasks[0].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("4 Jul 2019 09:00")));
                feature.Requirements[0].Tasks[1].Logs.Add(GlobalCommon.NewTaskLog(DateTime.Parse("4 Jul 2019 11:00")));
                feature.Requirements[0].Tasks[0].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for TaskLog 1 for Task 17" });
                feature.Requirements[0].Tasks[1].Logs[0].Comments.Add(new TaskLogCommentAPI { Comment = "Comment 1 for TaskLog 1 for Task 18" });
                feature.Requirements[0].Tasks[0].Logs[0].CheckIns.Add(
                    GlobalCommon.NewCheckIn(3, DateTime.Parse("4 Jul 2019 10:45"), codeBranchId, feature.Requirements[0].Tasks[0].ID));

                feature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(feature)));

                FeatureAPI fetched = FeatureAPI.From(new FeatureBF(db).Get(7));
                CheckObjectCount(fetched, 2, 3, 5, 2, 2, 2, 1, 1);
            }
        }

        private void CheckObjectCount(
            FeatureAPI feature,
            int requirementCount,
            int requirementCommentCount,
            int taskCount,
            int taskCommentCount,
            int taskLogCount,
            int taskLogCommentCount,
            int taskLogCheckInCount,
            int taskCheckinCount)
        {
            if (feature.Requirements != null)
            {
                foreach (var requirement in feature.Requirements)
                {
                    requirementCount--;
                    if (requirement.Comments != null)
                    {
                        foreach (var reqComment in requirement.Comments)
                        {
                            requirementCommentCount--;
                        }
                    }
                    if (requirement.Tasks != null)
                    {
                        foreach (var task in requirement.Tasks)
                        {
                            taskCount--;
                            if (task.Comments != null)
                            {
                                foreach (var taskComment in task.Comments)
                                {
                                    taskCommentCount--;
                                }
                            }
                            if (task.Logs != null)
                            {
                                foreach (var log in task.Logs)
                                {
                                    taskLogCount--;
                                    if (log.Comments != null)
                                    {
                                        foreach (var logComment in log.Comments)
                                        {
                                            taskLogCommentCount--;
                                        }
                                    }
                                    if (log.CheckIns != null)
                                    {
                                        foreach (var checkIn in log.CheckIns)
                                        {
                                            taskLogCheckInCount--;
                                        }
                                    }
                                }
                            }
                            if (task.CheckIns != null)
                            {
                                foreach (var taskCheckIn in task.CheckIns)
                                {
                                    taskCheckinCount--;
                                }
                            }
                        }
                    }
                }
            }

            Assert.AreEqual(0, requirementCount, "<1> Requirement count is incorrect");
            Assert.AreEqual(0, requirementCommentCount, "<2> Requriement comment count is incorrect");
            Assert.AreEqual(0, taskCount, "<3> Task count is incorrect");
            Assert.AreEqual(0, taskCommentCount, "<4> Task comment count is incorrect");
            Assert.AreEqual(0, taskLogCount, "<5> Task log count is incorrect");
            Assert.AreEqual(0, taskLogCommentCount, "<6> Log comment count is incorrect");
            Assert.AreEqual(0, taskLogCheckInCount, "<7> Check In count is incorrect");
            Assert.AreEqual(0, taskCheckinCount, "<8> Checkin count is incorrect");
            
        }

        private void CompareFeatureToFetchedFeature(FeatureAPI feature, FeatureAPI fetchedFeature)
        {
            Assert.AreEqual(feature.Title, fetchedFeature.Title);
            Assert.AreEqual(feature.Status, fetchedFeature.Status);
            Assert.AreEqual(2, fetchedFeature.Requirements.Count);
            for (int i = 0; i < 2; i++)     //  Requirements
            {
                int j = feature.Requirements[i]?.Comments?.Count ?? 0;
                if (j > 0)
                {
                    Assert.AreEqual(j, fetchedFeature.Requirements[i].Comments.Count);
                    for (int k = 0; k < j; k++)     //  RequirementComments
                    {
                        Assert.AreEqual(feature.Requirements[i].Comments[k].Comment,
                                        fetchedFeature.Requirements[i].Comments[k].Comment);
                    }
                }
                j = feature.Requirements[i]?.Tasks?.Count ?? 0;
                if (j > 0)
                {
                    Assert.AreEqual(j, fetchedFeature.Requirements[i].Tasks.Count);
                    for (int k = 0; k < j; k++)     //  Tasks
                    {
                        Assert.AreEqual(feature.Requirements[i].Tasks[k].IsActive,
                                        fetchedFeature.Requirements[i].Tasks[k].IsActive);
                        Assert.AreEqual(feature.Requirements[i].Tasks[k].TaskType,
                                        fetchedFeature.Requirements[i].Tasks[k].TaskType);
                        Assert.AreEqual(feature.Requirements[i].Tasks[k].Title,
                                        fetchedFeature.Requirements[i].Tasks[k].Title);

                        int l = feature.Requirements[i]?.Tasks[k]?.Comments?.Count ?? 0;
                        if (l > 0)
                        {
                            for (int m = 0; m < l; m++)     //  TaskComments
                            {
                                Assert.AreEqual(feature.Requirements[i].Tasks[k].Comments[m].Comment,
                                                fetchedFeature.Requirements[i].Tasks[k].Comments[m].Comment);
                            }
                        }

                        l = feature.Requirements[i]?.Tasks[k]?.Logs?.Count ?? 0;
                        if (l > 0)
                        {
                            for (int m = 0; m < l; m++)     //  TaskLogs
                            {
                                Assert.AreEqual(feature.Requirements[i].Tasks[k].Logs[m].Description,
                                                fetchedFeature.Requirements[i].Tasks[k].Logs[m].Description);
                                Assert.AreEqual(feature.Requirements[i].Tasks[k].Logs[m].EndTime,
                                                fetchedFeature.Requirements[i].Tasks[k].Logs[m].EndTime);
                                Assert.AreEqual(feature.Requirements[i].Tasks[k].Logs[m].LogDate,
                                                fetchedFeature.Requirements[i].Tasks[k].Logs[m].LogDate);
                                Assert.AreEqual(feature.Requirements[i].Tasks[k].Logs[m].StartTime,
                                                fetchedFeature.Requirements[i].Tasks[k].Logs[m].StartTime);

                                int n = feature.Requirements[i].Tasks[k].Logs[m]?.Comments?.Count ?? 0;
                                if (n > 0)
                                {
                                    for (int o = 0; o < n; o++)     //  TaskLogComments
                                    {
                                        Assert.AreEqual(feature.Requirements[i].Tasks[k].Logs[m].Comments[o].Comment,
                                                        fetchedFeature.Requirements[i].Tasks[k].Logs[m].Comments[o].Comment);
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}
