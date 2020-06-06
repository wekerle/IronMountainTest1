using DataAccessLayer.DbModels;

namespace DataAccessLayer.DbRepository
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User GetUser(int userId);
        void AddUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
    }
}
