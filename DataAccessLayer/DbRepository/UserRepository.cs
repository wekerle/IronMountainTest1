
using DataAccessLayer.DbModels;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.DbRepository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        public UserRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertUser", connection);

                SqlParameter paramUserName= new SqlParameter();
                paramUserName.ParameterName = "@UserName";
                paramUserName.SqlDbType = SqlDbType.NVarChar;
                paramUserName.Value = user.Username;
                cmd.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.SqlDbType = SqlDbType.NVarChar;
                paramPassword.Value = user.PasswordHash;
                cmd.Parameters.Add(paramPassword);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", connection);

                SqlParameter paramUserId = new SqlParameter();
                paramUserId.ParameterName = "@Id";
                paramUserId.SqlDbType = SqlDbType.NVarChar;
                paramUserId.Value = id;
                cmd.Parameters.Add(paramUserId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User GetUser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUser", connection);

                SqlParameter paramUserId = new SqlParameter();
                paramUserId.ParameterName = "@Id";
                paramUserId.SqlDbType = SqlDbType.NVarChar;
                paramUserId.Value = userId;
                cmd.Parameters.Add(paramUserId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new User
                        {
                            Username = dr.GetString(0),
                            PasswordHash = dr.GetString(1)
                        };
                    }
                }
                else
                {
                    return null;
                }

                dr.Close();
                connection.Close();

            }
            return null;
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", connection);

                SqlParameter paramUserName = new SqlParameter();
                paramUserName.ParameterName = "@UserName";
                paramUserName.SqlDbType = SqlDbType.NVarChar;
                paramUserName.Value = user.Username;
                cmd.Parameters.Add(paramUserName);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.SqlDbType = SqlDbType.NVarChar;
                paramPassword.Value = user.Username;
                cmd.Parameters.Add(paramPassword);

                SqlParameter paramUserId = new SqlParameter();
                paramUserId.ParameterName = "@Id";
                paramUserId.SqlDbType = SqlDbType.NVarChar;
                paramUserId.Value = user.UserId;
                cmd.Parameters.Add(paramUserId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserByUsername", connection);

                SqlParameter paramUsername = new SqlParameter();
                paramUsername.ParameterName = "@Username";
                paramUsername.SqlDbType = SqlDbType.NVarChar;
                paramUsername.Value = username;
                cmd.Parameters.Add(paramUsername);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new User {
                            Username = dr.GetString(0),
                            PasswordHash = dr.GetString(1)
                        };
                    }
                }
                else
                {
                    return null;
                }

                dr.Close();
                connection.Close();

            }
            return null;
        }
    }
}
