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
                FeatureAPI newFeature = new FeatureAPI
                {
                    ID = 5,
                    IsNew = true,
                    Status = RequirementStatus.Active,
                    Title = "Feature Number 5",
                    Requirements = new List<RequirementAPI>
                    {
                        new RequirementAPI
                        {
                            ID = 7,
                            IsNew = true,
                            Status = RequirementStatus.Active,
                            Title = "Requirement Number 7",
                            Comments = new List<RequirementCommentAPI>
                            {
                                new RequirementCommentAPI { Comment = "Comment Number 1" },
                                new RequirementCommentAPI { Comment = "Comment Number 2" }
                            },
                            Tasks = new List<TaskAPI>
                            {
                                new TaskAPI
                                {
                                    ID = 11,
                                    IsActive = true,
                                    IsNew = true,
                                    TaskType = TaskType.Task,
                                    Title = "Task Number 11",
                                    Comments = new List<TaskCommentAPI>
                                    {
                                        new TaskCommentAPI { Comment = "Comment Number 1" },
                                        new TaskCommentAPI { Comment = "Comment Number 2" }
                                    },
                                    Logs = new List<TaskLogAPI>
                                    {
                                        new TaskLogAPI
                                        {
                                            Description = "TaskLog Number 2",
                                            LogDate = DateTime.Parse("1 Jan 2019"),
                                            StartTime = TimeSpan.Parse("09:00"),
                                            EndTime = TimeSpan.Parse("11:00"),
                                            Comments = new List<TaskLogCommentAPI>
                                            {
                                                new TaskLogCommentAPI { Comment = "TaskLog Comment Number 1"},
                                                new TaskLogCommentAPI { Comment = "TaskLog Comment Number 2"}
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        new RequirementAPI
                        {
                            ID = 8,
                            IsNew = true,
                            Status = RequirementStatus.Proposed,
                            Title = "Requirement Number 8",
                        }
                    }
                };

                newFeature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(newFeature)));
                Assert.IsFalse(newFeature.IsNew);
                Assert.IsFalse(newFeature.Requirements[0].IsNew);
                Assert.IsFalse(newFeature.Requirements[1].IsNew);
                Assert.IsFalse(newFeature.Requirements[0].Tasks[0].IsNew);

                FeatureAPI fetched = FeatureAPI.From(new FeatureBF(db).Get(5));

                Assert.AreEqual(newFeature.Title, fetched.Title);
                Assert.AreEqual(newFeature.Status, fetched.Status);
                Assert.AreEqual(2, fetched.Requirements.Count);
                for (int i = 0; i < 2; i++)     //  Requirements
                {
                    int j = newFeature.Requirements[i]?.Comments?.Count ?? 0;
                    if (j > 0)
                    {
                        Assert.AreEqual(j, fetched.Requirements[i].Comments.Count);
                        for (int k = 0; k < j; k++)     //  RequirementComments
                        {
                            Assert.AreEqual(newFeature.Requirements[i].Comments[k].Comment,
                                            fetched.Requirements[i].Comments[k].Comment);
                        }
                    }
                    j = newFeature.Requirements[i]?.Tasks?.Count ?? 0;
                    if (j > 0)
                    {
                        Assert.AreEqual(j, fetched.Requirements[i].Tasks.Count);
                        for (int k = 0; k < j; k++)     //  Tasks
                        {
                            Assert.AreEqual(newFeature.Requirements[i].Tasks[k].IsActive,
                                            fetched.Requirements[i].Tasks[k].IsActive);
                            Assert.AreEqual(newFeature.Requirements[i].Tasks[k].TaskType,
                                            fetched.Requirements[i].Tasks[k].TaskType);
                            Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Title,
                                            fetched.Requirements[i].Tasks[k].Title);

                            int l = newFeature.Requirements[i]?.Tasks[k]?.Comments?.Count ?? 0;
                            if (l > 0)
                            {
                                for (int m = 0; m < l; m++)     //  TaskComments
                                {
                                    Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Comments[m].Comment,
                                                    fetched.Requirements[i].Tasks[k].Comments[m].Comment);
                                }
                            }

                            l = newFeature.Requirements[i]?.Tasks[k]?.Logs?.Count ?? 0;
                            if (l > 0)
                            {
                                for (int m = 0; m < l; m++)     //  TaskLogs
                                {
                                    Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Logs[m].Description,
                                                    fetched.Requirements[i].Tasks[k].Logs[m].Description);
                                    Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Logs[m].EndTime,
                                                    fetched.Requirements[i].Tasks[k].Logs[m].EndTime);
                                    Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Logs[m].LogDate,
                                                    fetched.Requirements[i].Tasks[k].Logs[m].LogDate);
                                    Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Logs[m].StartTime,
                                                    fetched.Requirements[i].Tasks[k].Logs[m].StartTime);

                                    int n = newFeature.Requirements[i].Tasks[k].Logs[m]?.Comments?.Count ?? 0;
                                    if (n > 0)
                                    {
                                        for (int o = 0; o < n; o++)     //  TaskLogComments
                                        {
                                            Assert.AreEqual(newFeature.Requirements[i].Tasks[k].Logs[m].Comments[o].Comment,
                                                            fetched.Requirements[i].Tasks[k].Logs[m].Comments[o].Comment);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

            }
        }

        [Test]
        public void FeatureTest2()
        {
            FeatureAPI newFeature = new FeatureAPI
            {
                ID = 6,
                IsNew = true,
                Status = RequirementStatus.Active,
                Title = "Feature Number 6"
            };

            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                newFeature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(newFeature)));

                Assert.IsFalse(newFeature.IsNew);

                newFeature.Requirements = new List<RequirementAPI>
                {
                    new RequirementAPI
                    {
                        ID = 9,
                        IsNew = true,
                        Status = RequirementStatus.Active,
                        Title = "Requirement Number 9",
                        FeatureID = 6,
                        Comments = new List<RequirementCommentAPI>
                        {
                            new RequirementCommentAPI { Comment = "Comment 1 for Requirement 9"},
                            new RequirementCommentAPI { Comment = "Comment 2 for Requirement 9"}
                        },
                        Tasks = new List<TaskAPI>
                        {
                            new TaskAPI
                            {
                                ID = 12,
                                IsActive = true,
                                IsNew = true,
                                TaskType = TaskType.Task,
                                Title = "Task Number 12",
                                Comments = new List<TaskCommentAPI>()
                                {
                                    new TaskCommentAPI { Comment = "Comment 1 for Task 12" },
                                    new TaskCommentAPI { Comment = "Comemnt 2 for Task 12" }
                                },
                                Logs = new List<TaskLogAPI>
                                {
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("09:00"),
                                        EndTime = TimeSpan.Parse("11:30"),
                                        Description = "Time Log 1 for Task 12",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 1 for Task 12" },
                                            new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 1 for Task 12"  }
                                        }
                                    },
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("11:30"),
                                        EndTime = TimeSpan.Parse("12:30"),
                                        Description = "Time Log 2 for Task 12",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 2 for Task 12" },
                                            new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 2 for Task 12"  }
                                        }
                                    }
                                }

                            },
                            new TaskAPI
                            {
                                ID = 13,
                                IsActive = true,
                                IsNew = true,
                                TaskType = TaskType.Bug,
                                Title = "Bug Number 13",
                                Comments = new List<TaskCommentAPI>()
                                {
                                    new TaskCommentAPI { Comment = "Comment 1 for Bug 13" }
                                },
                                Logs = new List<TaskLogAPI>
                                {
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("09:00"),
                                        EndTime = TimeSpan.Parse("11:30"),
                                        Description = "Time Log 1 for Bug 13",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 1 for Bug 13" },
                                            new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 1 for Bug 13"  }
                                        }
                                    }
                                }
                            },
                            new TaskAPI
                            {
                                ID = 14,
                                IsActive = true,
                                IsNew = true,
                                TaskType = TaskType.Bug,
                                Title = "Bug Number 14",
                                Comments = new List<TaskCommentAPI>()
                                {
                                    new TaskCommentAPI { Comment = "Comment 1 for Bug 14" }
                                },
                                Logs = new List<TaskLogAPI>
                                {
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("09:00"),
                                        EndTime = TimeSpan.Parse("11:30"),
                                        Description = "Time Log 1 for Bug 14",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 1 for Bug 14" },
                                            new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 1 for Bug 14"  }
                                        }
                                    },
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("11:30"),
                                        EndTime = TimeSpan.Parse("12:30"),
                                        Description = "Time Log 2 for Bug 14",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Time Log 2 for Bug 14" },
                                            new TaskLogCommentAPI { Comment = "Comment 2 for Time Log 2 for Bug 14"  }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));
                CheckObjectCount(newFeature, 1, 2, 3, 4, 5, 10, 0, 0);
                Assert.IsFalse(newFeature.Requirements[0].IsNew);

                newFeature.Requirements.Add(
                    new RequirementAPI
                    {
                        ID = 10,
                        Title = "Requirement Number 10",
                        Status = RequirementStatus.Active,
                        IsNew = true,
                        Comments = new List<RequirementCommentAPI>
                        {
                            new RequirementCommentAPI { Comment = "Comment for Requirement Number 10"}
                        },
                        Tasks = new List<TaskAPI>
                        {
                            new TaskAPI
                            {
                                ID = 15,
                                IsActive = true,
                                IsNew = true,
                                TaskType = TaskType.Task,
                                Title = "Task Number 15",
                                Comments = new List<TaskCommentAPI>
                                {
                                    new TaskCommentAPI { Comment = "Task Comment for Task 15"}
                                },
                                Logs = new List<TaskLogAPI>
                                {
                                    new TaskLogAPI
                                    {
                                        LogDate = DateTime.Parse("1 Jan 2019"),
                                        StartTime = TimeSpan.Parse("09:00"),
                                        EndTime = TimeSpan.Parse("12:30"),
                                        Description = "Task Log 1 for Task 15",
                                        Comments = new List<TaskLogCommentAPI>
                                        {
                                            new TaskLogCommentAPI { Comment = "Comment 1 for Task 1 for Task Number 15"}
                                        }
                                    }
                                }
                            }
                        }
                    });

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));

                FeatureAPI fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(newFeature, 2, 3, 4, 5, 6, 11, 0, 0);


                newFeature.Requirements[0].Comments.Add(new RequirementCommentAPI { Comment = "Comment 3 for Requirement 9" });
                newFeature.Requirements[0].Title = "Updated Requirement Number 9";
                newFeature.Requirements[0].Comments[0].Comment = "Updated Comment for Requirement Number 9";
                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(newFeature, 2, 4, 4, 5, 6, 11, 0, 0);
                Assert.AreEqual("Comment 3 for Requirement 9", fetchedFeature.Requirements[0].Comments[2].Comment);
                Assert.AreEqual("Updated Requirement Number 9", fetchedFeature.Requirements[0].Title);
                Assert.AreEqual("Updated Comment for Requirement Number 9", fetchedFeature.Requirements[0].Comments[0].Comment);

                newFeature.Requirements[1].Tasks.Add(new TaskAPI
                {
                    ID = 16,
                    Title = "Bug Number 16",
                    IsActive = true,
                    TaskType = TaskType.Bug,
                    IsNew = true
                });

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(    newFeature, 2, 4, 5, 5, 6, 11, 0, 0);
                CheckObjectCount(fetchedFeature, 2, 4, 5, 5, 6, 11, 0, 0);

                newFeature.Requirements[1].Tasks[1].Comments = new List<TaskCommentAPI>
                {
                    new TaskCommentAPI { Comment = "Comment 1 for Bug 16" },
                    new TaskCommentAPI { Comment = "Comment 2 for Bug 16" }
                };
                newFeature.Requirements[1].Tasks[1].Logs = new List<TaskLogAPI>
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
                                    new TaskCheckInAPI { TaskID = newFeature.Requirements[1].Tasks[1].ID }
                                }
                            }
                        }
                    }
                };

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));

                fetchedFeature = FeatureAPI.From(new FeatureBF(db).Get(6));
                CheckObjectCount(    newFeature, 2, 4, 5, 7, 8, 13, 1, 1);
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
    }
}
