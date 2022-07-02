namespace ActivityAppLibrary.Data
{
    public interface IActivityData
    {
        Task CreateActivity(ActivityModel activityModel);
        Task<List<ActivityModel>> GetActivities();
    }
}