using JobLogger.DAL;
using JobLogger.DAL.Common;
using System.Collections.Generic;

namespace JobLogger.API.Model
{
    public class RequirementAPI : APIBase
    {
        public string Title { get; set; }
        public RequirementStatus Status { get; set; }
        public long? FeatureID { get; set; }
        public FeatureAPI Feature { get; set; }
        public virtual ICollection<TaskAPI> Tasks { get; set; }

        public static Requirement To(RequirementAPI item, bool includeTasks = true)
        {
            if (item != null)
            {
                return new Requirement
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Tasks = includeTasks ? TaskAPI.To(item.Tasks) : null,
                    FeatureID = item.FeatureID,
                    Feature = FeatureAPI.To(item.Feature),
                    IsNew = item.IsNew
                };
            }
            else
            {
                return null;
            }
        }

        public static RequirementAPI From(Requirement item, bool includeTasks = true)
        {
            if (item != null)
            {
                return new RequirementAPI
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Tasks = includeTasks ? TaskAPI.From(item.Tasks) : null,
                    FeatureID = item.FeatureID,
                    Feature = FeatureAPI.From(item.Feature, false),
                    IsNew = item.IsNew
                };
            }
            else
            {
                return null;
            }
        }

        public static ICollection<Requirement> To(ICollection<RequirementAPI> items)
        {
            if (items != null)
            {
                ICollection<Requirement> list = new List<Requirement>();
                foreach (var item in items)
                {
                    list.Add(To(item));
                }
                return list;
            }
            else
            {
                return null;
            }
        }

        public static ICollection<RequirementAPI> From(ICollection<Requirement> items)
        {
            if (items != null)
            {
                ICollection<RequirementAPI> list = new List<RequirementAPI>();
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
