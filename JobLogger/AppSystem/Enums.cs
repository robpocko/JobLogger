namespace JobLogger.AppSystem
{
    public enum RequirementStatus
    {
        All = 0,
        Proposed = 1,
        Active,
        Resolved,
        Closed
    }

    public enum TaskType
    {
        All = 0,
        Task = 1,
        Bug
    }
}
