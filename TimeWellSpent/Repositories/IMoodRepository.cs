using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public interface IMoodRepository
    {
        void DeleteMood(int id);
        List<Mood> GetAllMoods();
        Mood GetMoodById(int id);
        void InsertMood(Mood mood);
        void UpdateMood(Mood mood);
    }
}