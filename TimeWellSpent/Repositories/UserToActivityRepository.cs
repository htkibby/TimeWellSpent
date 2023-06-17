using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class UserToActivityRepository : BaseRepository, IUserToActivityRepository
    {
        public UserToActivityRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserToActivity> GetAllUserToActivities()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
	                                        [id],
	                                        userId AS 'UserId',
	                                        activiyId AS 'ActivityId',
	                                        moodId AS 'MoodId',
	                                        categoryId AS 'CategoryId',
	                                        weekId AS 'WeekId',
	                                        hoursSpent AS 'HoursSpent',
	                                        [description] AS 'Description',
                                            createdBy AS 'CreatedBy'
	                                        FROM UserToActivity";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<UserToActivity> joins = new();

                    while (reader.Read())
                    {
                        joins.Add(new UserToActivity()
                        {
                            Id = DbUtils.GetInt(reader, "id"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            ActivityId = DbUtils.GetInt(reader, "ActivityId"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            MoodId = DbUtils.GetInt(reader, "MoodId"),
                            WeekId = DbUtils.GetInt(reader, "WeeKId"),
                            Description = DbUtils.GetString(reader, "Description"),
                            HoursSpent = DbUtils.GetDecimal(reader, "HoursSpent"),
                            CreatedBy = DbUtils.GetString(reader, "CreatedBy")
                        });
                    }

                    reader.Close();
                    return joins;
                }
            }
        }

        public UserToActivity GetUserToActivityById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
	                                        [id],
	                                        userId AS 'UserId',
	                                        activiyId AS 'ActivityId',
	                                        moodId AS 'MoodId',
	                                        categoryId AS 'CategoryId',
	                                        weekId AS 'WeekId',
	                                        hoursSpent AS 'HoursSpent',
	                                        [description] AS 'Description',
                                            createdBy AS 'CreatedBy'
	                                        FROM UserToActivity
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    UserToActivity join = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "UserToActivityId"))
                    {
                        join = new UserToActivity()
                        {
                            Id = DbUtils.GetInt(reader, "UserToActivityId"),
                            UserId = DbUtils.GetInt(reader, "UserId"),
                            ActivityId = DbUtils.GetInt(reader, "ActivityId"),
                            CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                            MoodId = DbUtils.GetInt(reader, "MoodId"),
                            WeekId = DbUtils.GetInt(reader, "WeeKId"),
                            Description = DbUtils.GetString(reader, "Description"),
                            HoursSpent = DbUtils.GetDecimal(reader, "HoursSpent"),
                            CreatedBy = DbUtils.GetString(reader, "CreatedBy")
                        };
                    }
                    reader.Close();
                    return join;
                }
            }
        }

        public void InsertUserToActivity(UserToActivity join)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserToActivity
                                                    (userId,
                                                    activiyId,
                                                    moodId,
                                                    categoryId,
                                                    weekId,
                                                    hoursSpent,
                                                    createdBy,
                                                    [description])
                                        OUTPUT INSERTED.id
                                        VALUES (@userId, @activityId, @moodId, @categoryId, @weekId, @hoursSpent, @createdBy, @description)";
                    cmd.Parameters.AddWithValue("@userId", join.UserId);
                    cmd.Parameters.AddWithValue("@activityId", join.ActivityId);
                    cmd.Parameters.AddWithValue("@moodId", join.MoodId);
                    cmd.Parameters.AddWithValue("@categoryId", join.CategoryId);
                    cmd.Parameters.AddWithValue("@weekId", join.WeekId);
                    cmd.Parameters.AddWithValue("@hoursSpent", join.HoursSpent);
                    cmd.Parameters.AddWithValue("@description", join.Description);
                    cmd.Parameters.AddWithValue("@createdBy", join.CreatedBy);
                    join.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateUserToActivity(UserToActivity join)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserToActivity SET
                                        userId = @userId,
                                        activiyId = @activityId,
                                        moodId = @moodId,
                                        categoryId = @categoryId,
                                        weekId = @weekId,
                                        hoursSpent = @hoursSpent,
                                        [description] = @description
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@userId", join.UserId);
                    cmd.Parameters.AddWithValue("@activityId", join.ActivityId);
                    cmd.Parameters.AddWithValue("@moodId", join.MoodId);
                    cmd.Parameters.AddWithValue("@categoryId", join.CategoryId);
                    cmd.Parameters.AddWithValue("@weekId", join.WeekId);
                    cmd.Parameters.AddWithValue("@hoursSpent", join.HoursSpent);
                    cmd.Parameters.AddWithValue("@description", join.Description);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserToActivity(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM UserToActivity WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
