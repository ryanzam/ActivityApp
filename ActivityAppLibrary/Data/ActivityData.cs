namespace ActivityAppLibrary.Data
{
    public class ActivityData : IActivityData
    {
        public readonly IMongoCollection<ActivityModel> _activities;
        private readonly IMemoryCache _memoryCache;
        private const string CacheName = "Activity";

        public ActivityData(IDbConnection dbConnection, IMemoryCache memoryCache)
        {
            _activities = dbConnection.ActivityCollection;
            _memoryCache = memoryCache;
        }

        public async Task<List<ActivityModel>> GetActivities()
        {
            var cachedData = _memoryCache.Get<List<ActivityModel>>(CacheName);
            if (cachedData is null)
            {
                var res = await _activities.FindAsync(_ => true);
                cachedData = res.ToList();
                _memoryCache.Set(CacheName, cachedData, TimeSpan.FromHours(2));
            }
            return cachedData;
        }

        public Task CreateActivity(ActivityModel activityModel)
        {
            return _activities.InsertOneAsync(activityModel);
        }
    }
}
