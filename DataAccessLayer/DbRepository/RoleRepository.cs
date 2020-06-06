
using DataAccessLayer.DbModels;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.DbRepository
{
    public class RoleRepository : IRoleRepository
    {
        private string _connectionString;
        public RoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertRole", connection);

                SqlParameter paramDescription = new SqlParameter();
                paramDescription.ParameterName = "@Description";
                paramDescription.SqlDbType = SqlDbType.NVarChar;
                paramDescription.Value = role.Description;
                cmd.Parameters.Add(paramDescription);

                SqlParameter paramUserId = new SqlParameter();
                paramUserId.ParameterName = "@UserId";
                paramUserId.SqlDbType = SqlDbType.Int;
                paramUserId.Value = role.UserId;
                cmd.Parameters.Add(paramUserId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DeleteRole(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteRole", connection);

                SqlParameter paramRoleId = new SqlParameter();
                paramRoleId.ParameterName = "@Id";
                paramRoleId.SqlDbType = SqlDbType.NVarChar;
                paramRoleId.Value = id;
                cmd.Parameters.Add(paramRoleId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Role GetRole(int roleId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetRole", connection);

                SqlParameter paramRoleId = new SqlParameter();
                paramRoleId.ParameterName = "@Id";
                paramRoleId.SqlDbType = SqlDbType.NVarChar;
                paramRoleId.Value = roleId;
                cmd.Parameters.Add(paramRoleId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        return new Role
                        {
                            Description = dr.GetString(0),
                            UserId = dr.GetInt32(1)
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

        public void UpdateRole(Role role)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateRole", connection);

                SqlParameter paramDescription = new SqlParameter();
                paramDescription.ParameterName = "@Description";
                paramDescription.SqlDbType = SqlDbType.NVarChar;
                paramDescription.Value = role.Description;
                cmd.Parameters.Add(paramDescription);

                SqlParameter paramUserId = new SqlParameter();
                paramUserId.ParameterName = "@UserId";
                paramUserId.SqlDbType = SqlDbType.Int;
                paramUserId.Value = role.UserId;
                cmd.Parameters.Add(paramUserId);

                SqlParameter paramRoleId = new SqlParameter();
                paramRoleId.ParameterName = "@Id";
                paramRoleId.SqlDbType = SqlDbType.Int;
                paramRoleId.Value = role.RoleId;
                cmd.Parameters.Add(paramRoleId);

                connection.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
