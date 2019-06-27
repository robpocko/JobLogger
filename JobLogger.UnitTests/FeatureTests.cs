using JobLogger.BF;
using JobLogger.DAL;
using JobLogger.DAL.Common;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using JobLogger.API.Model;

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
            FeatureAPI newFeature = new FeatureAPI
            {
                ID = 5,
                IsNew = true,
                Status = RequirementStatus.Active,
                Title = "Feature Number 5"
            };

            using (JobLoggerDbContext db = new JobLoggerDbContext())
            {
                newFeature = FeatureAPI.From(new FeatureBF(db).Create(FeatureAPI.To(newFeature)));

                Assert.IsFalse(newFeature.IsNew);

                newFeature.Requirements = new List<RequirementAPI>
                {
                    new RequirementAPI
                    {
                        ID = 7,
                        IsNew = true,
                        Status = RequirementStatus.Active,
                        Title = "Requirement Number 7",
                        FeatureID = 5
                    }
                };

                newFeature = FeatureAPI.From(new FeatureBF(db).Update(FeatureAPI.To(newFeature)));
                Assert.AreEqual(1, newFeature.Requirements.Count);
                Assert.IsFalse(newFeature.Requirements.ElementAt(0).IsNew);
            }

        }
    }
}
