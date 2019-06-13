using JobLogger.DAL;
using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class FeatureAPI : APIBase
    {
        public string Title { get; set; }
        public RequirementStatus Status { get; set; }
        public virtual ICollection<RequirementAPI> Requirements { get; set; }

        public static Feature To(FeatureAPI item, bool includeRequirements = true)
        {
            if (item != null)
            {
                return new Feature
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Requirements = includeRequirements ? RequirementAPI.To(item.Requirements) : null,
                    IsNew = item.IsNew
                };
            }
            else
            {
                return null;
            }
        }

        public static FeatureAPI From(Feature item, bool includeTasks = true)
        {
            if (item != null)
            {
                return new FeatureAPI
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Requirements = includeTasks ? RequirementAPI.From(item.Requirements) : null,
                    IsNew = item.IsNew
                };
            }
            else
            {
                return null;
            }

        }

        public static ICollection<FeatureAPI> From(ICollection<Feature> items)
        {
            if (items != null)
            {
                ICollection<FeatureAPI> list = new List<FeatureAPI>();
                foreach (var item in items)
                {
                    list.Add(From(item));
                }
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
