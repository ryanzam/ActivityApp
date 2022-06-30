namespace ActivityAppLibrary.Data
{
    public class UserData : IUserData
    {
        public readonly IMongoCollection<UserModel> _users;
        public UserData(IDbConnection dbConnection)
        {
            _users = dbConnection.UserCollection;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var res = await _users.FindAsync(_ => true);
            return res.ToList();
        }

        public async Task<UserModel> GetUser(string id)
        {
            var res = await _users.FindAsync(usr => usr.Id == id);
            return res.FirstOrDefault();
        }

        public async Task<UserModel> GetUserFromAuzure(string id)
        {
            var res = await _users.FindAsync(usr => usr.ObjectID == id);
            return res.FirstOrDefault();
        }

        public Task CreateUser(UserModel newUser)
        {
            return _users.InsertOneAsync(newUser);
        }

        public Task UpdateUser(UserModel updatedUser)
        {
            var filterUsr = Builders<UserModel>.Filter.Eq("Id", updatedUser.Id);
            return _users.ReplaceOneAsync(filterUsr, updatedUser, new ReplaceOptions { IsUpsert = true });
        }
    }
}
