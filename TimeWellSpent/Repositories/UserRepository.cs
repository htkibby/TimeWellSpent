using Microsoft.Data.SqlClient;
using SpyDuhLakers.Utils;
using TimeWellSpent.Models;

namespace TimeWellSpent.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public List<User> GetAllUsers()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'UserId'
                                        ,[name] as 'UserName'
                                        ,email
                                        ,firebaseUid
                                        FROM [User]";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<User> users = new();

                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "email"),
                            FirebaseUid = DbUtils.GetString(reader, "firebaseUid")
                        });
                    }

                    reader.Close();
                    return users;
                }
            }
        }

        public User GetUserById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        id AS 'UserId'
                                        ,[name] as 'UserName'
                                        ,email
                                        ,firebaseUid
                                        FROM [User]
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    User user = null;

                    if (reader.Read() && DbUtils.IsNotDbNull(reader, "UserId"))
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "email"),
                            FirebaseUid = DbUtils.GetString(reader, "firebaseUid")
                        };
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        public void InsertUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [User]
                                        ([name], email, firebaseUid)
                                        OUTPUT INSERTED.id
                                        VALUES (@name, @email, @firebaseUid)";
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("@firebaseUid", user.FirebaseUid);
                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE [User] SET
                                        name = @name,
                                        email = @email,
                                        firebaseUid = @firebaseUid
                                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", user.Id);
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@email", user.Email);
                    cmd.Parameters.AddWithValue("firebaseUid", user.FirebaseUid);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM [User] WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
    }
}
