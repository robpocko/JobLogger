using JobLogger.DAL;
using JobLogger.DAL.Common;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.API.Model
{
    public class RequirementAPI : APITFS
    {
        public string                       Title { get; set; }
        public RequirementStatus            Status { get; set; }
        public long?                        FeatureID { get; set; }
        public FeatureAPI                   Feature { get; set; }
        public List<TaskAPI>                Tasks { get; set; }
        public List<RequirementCommentAPI>  Comments { get; set; }

        public static Requirement To(RequirementAPI item, bool includeTasks = true, bool includeComments = true)
        {
            if (item != null)
            {
                return new Requirement
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Tasks = includeTasks ? TaskAPI.To(item.Tasks) : null,
                    Comments = includeComments ? RequirementCommentAPI.To(item.Comments) : null,
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

        public static RequirementAPI From(Requirement item, bool includeTasks = true, bool includeComments = true)
        {
            if (item != null)
            {
                return new RequirementAPI
                {
                    ID = item.ID,
                    Title = item.Title,
                    Status = item.Status,
                    Tasks = includeTasks && item.Tasks != null ? TaskAPI.From(item.Tasks).ToList() : null,
                    Comments = includeComments && item.Comments != null ? RequirementCommentAPI.From(item.Comments).ToList() : null,
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
