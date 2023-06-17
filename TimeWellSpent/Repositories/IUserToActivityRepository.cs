using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface IUserToActivityRepository
    {
        void DeleteUserToActivity(int id);
        List<UserToActivity> GetAllUserToActivities();
        UserToActivity GetUserToActivityById(int id);
        void InsertUserToActivity(UserToActivity join);
        void UpdateUserToActivity(UserToActivity join);
    }
}