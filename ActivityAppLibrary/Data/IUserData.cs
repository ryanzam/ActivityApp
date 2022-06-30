namespace ActivityAppLibrary.Data
{
    public interface IUserData
    {
        Task CreateUser(UserModel newUser);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUser(string id);
        Task<UserModel> GetUserFromAuzure(string id);
        Task UpdateUser(UserModel updatedUser);
    }
}