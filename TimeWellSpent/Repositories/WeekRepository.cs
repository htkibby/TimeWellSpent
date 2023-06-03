using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class WeekRepository : BaseRepository, IWeekRepository
    {
        public WeekRepository(IConfiguration configuration) : base(configuration) { }

        public List<Week> GetAllActivities()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'WeekId'
                                        ,startDate as 'StartDate'
                                        ,endDate as 'EndDate'
                                        FROM Week";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Week> weeks = new();

                    while (reader.Read())
                    {
                        weeks.Add(new Week()
                        {
                            Id = DbUtils.GetInt(reader, "WeekId"),
                            StartDate = DbUtils.GetDateTime(reader, "WeekName"),
                            EndDate = DbUtils.GetDateTime(reader, "EndDate")
                        });
                    }

                    reader.Close();
                    return weeks;
                }
            }
        }

        public Week GetWeekById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'WeekId'
                                        ,startDate as 'StartDate'
                                        ,endDate as 'EndDate'
                                        FROM Week
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Week week = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "WeekId"))
                    {
                        week = new Week()
                        {
                            Id = DbUtils.GetInt(reader, "WeekId"),
                            StartDate = DbUtils.GetDateTime(reader, "WeekName"),
                            EndDate = DbUtils.GetDateTime(reader, "EndDate")
                        };
                    }
                    reader.Close();
                    return week;
                }
            }
        }

        public void InsertWeek(Week week)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Week
                                        (startDate, endDate)
                                        OUTPUT INSERTED.id
                                        VALUES (@startDate, @endDate)";
                    cmd.Parameters.AddWithValue("@startDate", week.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", week.EndDate);
                    week.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateWeek(Week week)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Week SET
                                        startDate = @startDate
                                        ,endDate = @endDate
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", week.Id);
                    cmd.Parameters.AddWithValue("@startDate", week.StartDate);
                    cmd.Parameters.AddWithValue("@endDate", week.EndDate);
                }
            }
        }

        public void DeleteWeek(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Week WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
