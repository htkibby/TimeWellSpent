using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface IActivityRepository
    {
        void DeleteActivity(int id);
        Activity GetActivityById(int id);
        List<Activity> GetActivityByUser(string email);
        List<Activity> GetAllActivities();
        void InsertActivity(Activity activity);
        void UpdateActivity(Activity activity);
    }
}