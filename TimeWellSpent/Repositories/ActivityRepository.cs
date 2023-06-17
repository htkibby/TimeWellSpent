using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(IConfiguration configuration) : base(configuration) { }

        public List<Activity> GetAllActivities()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'ActivityId'
                                        ,[name] as 'ActivityName'
                                        ,image
                                        FROM Activity";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Activity> activities = new();

                    while (reader.Read())
                    {
                        activities.Add(new Activity()
                        {
                            Id = DbUtils.GetInt(reader, "ActivityId"),
                            Name = DbUtils.GetString(reader, "ActivityName"),
                            Image = DbUtils.GetString(reader, "image")
                        });
                    }

                    reader.Close();
                    return activities;
                }
            }
        }

        public List<Activity> GetActivityByUser(string email)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        u.id AS 'UserId'
                                        ,u.[name] as 'UserName'
                                        ,u.email AS 'UserEmail'
                                        ,u.firebaseUid
                                        ,uta.dateOccured
                                        ,uta.[description]
                                        ,uta.hoursSpent
                                        ,m.[name] AS 'Mood'
                                        ,m.color AS 'Mood Color'
                                        ,c.[name] AS 'Category'
                                        ,a.id AS 'ActualActivityId'
                                        ,a.[name] AS 'Activity'
                                        ,a.[image] AS 'Activity Image'
                                        FROM Activity a
                                        JOIN UserToActivity uta ON a.id = uta.activiyId
                                        JOIN Mood m ON m.id = uta.moodId
                                        JOIN Category c ON c.id = uta.categoryId
                                        JOIN [User] u ON u.id = uta.userId
                                        WHERE u.email = @email";
                    cmd.Parameters.AddWithValue("email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Activity> activities = new();
                    Activity activity = null;

                    while (reader.Read())
                    {
                        activity= new Activity()
                        {
                            Id = DbUtils.GetInt(reader, "ActualActivityId"),
                            Name = DbUtils.GetString(reader, "Activity"),
                            Image = DbUtils.GetString(reader, "Activity Image"),
                            User = new User()
                            {
                                Name = DbUtils.GetString(reader, "UserName"),
                                Email = DbUtils.GetString(reader, "UserEmail")
                            },
                            Mood = new Mood()
                            {
                                Name = DbUtils.GetString(reader, "Mood"),
                                Color = DbUtils.GetString(reader, "Mood Color")
                            },
                            Category = new Category()
                            {
                                Name = DbUtils.GetString(reader, "Category")
                            }
                        };
                        activities.Add(activity);
                    }
                    conn.Close();
                    return activities;
                }
            }
        }

        public Activity GetActivityById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'ActivityId'
                                        ,[name] as 'ActivityName'
                                        ,image
                                        FROM Activity
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Activity activity = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "ActivityId"))
                    {
                        activity = new Activity()
                        {
                            Id = DbUtils.GetInt(reader, "ActivityId"),
                            Name = DbUtils.GetString(reader, "ActivityName"),
                            Image = DbUtils.GetString(reader, "image")
                        };
                    }
                    reader.Close();
                    return activity;
                }
            }
        }

        public void InsertActivity(Activity activity)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Activity
                                        ([name], image)
                                        OUTPUT INSERTED.id
                                        VALUES (@name, @image)";
                    cmd.Parameters.AddWithValue("@name", activity.Name);
                    cmd.Parameters.AddWithValue("@image", activity.Image);
                    activity.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateActivity(Activity activity)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Activity SET
                                        name = @name,
                                        image = @image
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", activity.Id);
                    cmd.Parameters.AddWithValue("@name", activity.Name);
                    cmd.Parameters.AddWithValue("@image", activity.Image);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteActivity(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Activity WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
