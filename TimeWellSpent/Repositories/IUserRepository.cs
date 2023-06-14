using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface IUserRepository
    {
        void DeleteUser(int id);
        List<User> GetAllUsers();
        User GetUserById(int id);
        void InsertUser(User user);
        void UpdateUser(User user);
    }
}