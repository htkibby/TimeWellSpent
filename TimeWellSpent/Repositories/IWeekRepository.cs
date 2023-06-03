using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface IWeekRepository
    {
        void DeleteWeek(int id);
        List<Week> GetAllActivities();
        Week GetWeekById(int id);
        void InsertWeek(Week week);
        void UpdateWeek(Week week);
    }
}