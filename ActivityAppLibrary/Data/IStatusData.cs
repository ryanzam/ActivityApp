namespace ActivityAppLibrary.Data
{
    public interface IStatusData
    {
        Task CreateNewStatus(StatusModel status);
        Task<List<StatusModel>> GetStatuses();
    }
}