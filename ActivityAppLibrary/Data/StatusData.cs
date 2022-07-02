namespace ActivityAppLibrary.Data
{
    public class StatusData : IStatusData
    {
        private readonly IMongoCollection<StatusModel> _status;
        private readonly IMemoryCache _memoryCache;

        private const string CacheName = "Status";

        public StatusData(IDbConnection dbConnection, IMemoryCache mememoryCache)
        {
            _status = dbConnection.StatusCollection;
            _memoryCache = mememoryCache;
        }

        public async Task<List<StatusModel>> GetStatuses()
        {
            var cachedData = _memoryCache.Get<List<StatusModel>>(CacheName);
            if (cachedData is null)
            {
                var res = await _status.FindAsync(_ => true);
                cachedData = res.ToList();
                _memoryCache.Set(CacheName, cachedData, TimeSpan.FromHours(2));
            }
            return cachedData;
        }

        public Task CreateNewStatus(StatusModel status)
        {
            return _status.InsertOneAsync(status);
        }
    }
}
