using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class MoodRepository : BaseRepository, IMoodRepository
    {
        public MoodRepository(IConfiguration configuration) : base(configuration) { }

        public List<Mood> GetAllMoods()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'MoodId'
                                        ,[name] as 'MoodName'
                                        ,color
                                        FROM Mood";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Mood> moods = new();

                    while (reader.Read())
                    {
                        moods.Add(new Mood()
                        {
                            Id = DbUtils.GetInt(reader, "MoodId"),
                            Name = DbUtils.GetString(reader, "MoodName"),
                            Color = DbUtils.GetString(reader, "color")
                        });
                    }

                    reader.Close();
                    return moods;
                }
            }
        }

        public Mood GetMoodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'MoodId'
                                        ,[name] as 'MoodName'
                                        ,color
                                        FROM Mood
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Mood mood = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "MoodId"))
                    {
                        mood = new Mood()
                        {
                            Id = DbUtils.GetInt(reader, "MoodId"),
                            Name = DbUtils.GetString(reader, "MoodName"),
                            Color = DbUtils.GetString(reader, "color")
                        };
                    }
                    reader.Close();
                    return mood;
                }
            }
        }

        public void InsertMood(Mood mood)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Mood
                                        ([name], color)
                                        OUTPUT INSERTED.id
                                        VALUES (@name, @color)";
                    cmd.Parameters.AddWithValue("@name", mood.Name);
                    cmd.Parameters.AddWithValue("color", mood.Color);
                    mood.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateMood(Mood mood)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Mood SET
                                        name = @name,
                                        color = @color
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", mood.Id);
                    cmd.Parameters.AddWithValue("@name", mood.Name);
                    cmd.Parameters.AddWithValue("@color", mood.Color);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMood(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Mood WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
