using JobLogger.DAL;
using System.Collections.Generic;
using System.Linq;

namespace JobLogger.API.Model
{
    public class CodeBranchAPI : APIBase
    {
        public string           Name { get; set; }
        public List<CheckInAPI> BranchCheckIns { get; set; }

        public static CodeBranch To(CodeBranchAPI item)
        {
            return new CodeBranch
            {
                ID = item.ID,
                Name = item.Name,
                BranchCheckIns = CheckInAPI.To(item.BranchCheckIns)
            };
        }

        public static CodeBranchAPI From(CodeBranch item)
        {
            return new CodeBranchAPI
            {
                ID = item.ID,
                Name = item.Name,
                BranchCheckIns = CheckInAPI.From(item.BranchCheckIns).ToList()
            };
        }

        public static ICollection<CodeBranchAPI> From(ICollection<CodeBranch> items)
        {
            if (items != null)
            {
                ICollection<CodeBranchAPI> list = new List<CodeBranchAPI>();
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
