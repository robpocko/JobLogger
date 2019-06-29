using JobLogger.API.Model;
using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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
                            new RequirementCommentAPI { Comment = "Comment 1 for Requirement 7"},
                            new RequirementCommentAPI { Comment = "Comment 2 for Requirement 7"}
                        }
                    }
                };

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));
                Assert.AreEqual(1, newFeature.Requirements.Count);
                Assert.IsFalse(newFeature.Requirements[0].IsNew);
            }
        }
    }
}
