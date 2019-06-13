using System;

namespace JobLogger.DAL.Common
{
    public enum RequirementStatus
    {
        Proposed = 1,
        Active,
        Resolved,
        Closed
    }

    public enum TaskType
    {
        Task = 1,
        Bug
    }
}
