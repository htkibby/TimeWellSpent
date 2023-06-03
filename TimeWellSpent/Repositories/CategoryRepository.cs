using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Category> GetAllActivities()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'CategoryId'
                                        ,[name] as 'CategoryName'
                                        FROM Category";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Category> activities = new();

                    while (reader.Read())
                    {
                        activities.Add(new Category()
                        {
                            Id = DbUtils.GetInt(reader, "CategoryId"),
                            Name = DbUtils.GetString(reader, "CategoryName")
                        });
                    }

                    reader.Close();
                    return activities;
                }
            }
        }

        public Category GetCategoryById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'CategoryId'
                                        ,[name] as 'CategoryName'
                                        FROM Category
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Category category = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "CategoryId"))
                    {
                        category = new Category()
                        {
                            Id = DbUtils.GetInt(reader, "CategoryId"),
                            Name = DbUtils.GetString(reader, "CategoryName")
                        };
                    }
                    reader.Close();
                    return category;
                }
            }
        }

        public void InsertCategory(Category activity)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Category
                                        ([name])
                                        OUTPUT INSERTED.id
                                        VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", activity.Name);
                    activity.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateCategory(Category activity)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Category SET
                                        name = @name
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", activity.Id);
                    cmd.Parameters.AddWithValue("@name", activity.Name);
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Category WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
