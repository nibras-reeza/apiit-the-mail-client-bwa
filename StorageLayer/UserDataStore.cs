using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Storage
{
    public class UserDataStore
    {
        //https://stackoverflow.com/questions/6536715/get-connection-string-from-app-config

        private static string connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"C:\\Users\\Nibras\\Dropbox\\APIIT\\Final Year\\BWA\\final\\final\\Project\\TheMailClient\\StorageLayer\\App_Data\\TMCDB.mdf\";Integrated Security=True";

        private static UserDataStore instance = new UserDataStore();

        public static UserDataStore getInstance()
        {
            return instance;
        }

        private UserDataStore()
        {
        }

        public string Add(UserDTO u)
        {       //http://docs.telerik.com/data-access/developers-guide/low-level-ado-api/executing-stored-procedures/data-access-tasks-adonet-stored-procedures-out-value-params
            SqlParameter res = new SqlParameter();
            res.Direction = ParameterDirection.Output;
            res.ParameterName = "@RESULT";
            res.Size = 20;
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REGISTER_USER";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", u.Password));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", u.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", u.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", u.Address));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", u.EMail));
                cmd.Parameters.Add(new SqlParameter("@PHONE", u.Phone));
                cmd.Parameters.Add(res);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SAVE_UI_CONFIG";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                cmd.Parameters.Add(new SqlParameter("@THEME", u.Theme));
                cmd.Parameters.Add(new SqlParameter("@TAG_WIDTH", u.TagWidth));
                cmd.Parameters.Add(new SqlParameter("@TAG_TOP", u.TagTop));
                cmd.Parameters.Add(new SqlParameter("@TAG_LEFT", u.TagLeft));
                cmd.Parameters.Add(new SqlParameter("@THREAD_WIDTH", u.ThreadWidth));
                cmd.Parameters.Add(new SqlParameter("@THREAD_TOP", u.ThreadTop));
                cmd.Parameters.Add(new SqlParameter("@THREAD_LEFT", u.ThreadLeft));
                cmd.Parameters.Add(new SqlParameter("@HEADER_HEIGHT", u.HeaderHeight));
                cmd.Parameters.Add(new SqlParameter("@LOCALE", u.Locale));
                con.Open();
                cmd.ExecuteNonQuery();
            }

            if (u.type.Equals(UserDTO.UserType.ADMIN))
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "MAKE_ADMIN";
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            return res.ToString();
        }

        public UserDTO Delete(string username)
        {
            UserDTO user = Find(username);
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DELETE_USER";
                cmd.Parameters.Add(new SqlParameter("@USER_NAME", username));
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return user;
        }

        public UserDTO Update(UserDTO u)
        {
            UserDTO user = Find(u.Username);
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPDATE_PASSWORD";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                cmd.Parameters.Add(new SqlParameter("@PASSWORD", u.Password));
                con.Open();
                cmd.ExecuteNonQuery();
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPDATE_USER";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", u.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", u.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", u.Address));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", u.EMail));
                cmd.Parameters.Add(new SqlParameter("@PHONE", u.Phone));
                con.Open();
                cmd.ExecuteNonQuery();
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SAVE_UI_CONFIG";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                cmd.Parameters.Add(new SqlParameter("@THEME", u.Theme));
                cmd.Parameters.Add(new SqlParameter("@TAG_WIDTH", u.TagWidth));
                cmd.Parameters.Add(new SqlParameter("@TAG_TOP", u.TagTop));
                cmd.Parameters.Add(new SqlParameter("@TAG_LEFT", u.TagLeft));
                cmd.Parameters.Add(new SqlParameter("@THREAD_WIDTH", u.ThreadWidth));
                cmd.Parameters.Add(new SqlParameter("@THREAD_TOP", u.ThreadTop));
                cmd.Parameters.Add(new SqlParameter("@THREAD_LEFT", u.ThreadLeft));
                cmd.Parameters.Add(new SqlParameter("@HEADER_HEIGHT", u.HeaderHeight));
                cmd.Parameters.Add(new SqlParameter("@LOCALE", u.Locale));
                con.Open();
                cmd.ExecuteNonQuery();
            }

            List<string> serverNs = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM NAMESPACES WHERE USERNAME='" + u.Username + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    serverNs.Add(read["NAMESPACE_ID"].ToString());
                }
            }

            foreach (string s in serverNs.Except(u.namespaces))
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "REMOVE_NS";
                    cmd.Parameters.Add(new SqlParameter("@NAMESPACE_ID", s));
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            foreach (string s in u.namespaces.Except(serverNs))
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ADD_NS";
                    cmd.Parameters.Add(new SqlParameter("@NAMESPACE_ID", s));
                    cmd.Parameters.Add(new SqlParameter("@USERNAME", u.Username));
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            return user;
        }

        public UserDTO Find(string username)
        {
            UserDTO u = new UserDTO();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM PEOPLE WHERE USERNAME='" + username + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    u.Username = read["USERNAME"].ToString();
                    u.FirstName = read["FIRST_NAME"].ToString();
                    u.LastName = read["LAST_NAME"].ToString();
                    u.EMail = read["EMAIL"].ToString();
                    u.Phone = read["PHONE"].ToString();
                    u.Address = read["ADDRESS"].ToString();
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM LOGINS WHERE USERNAME='" + username + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();
                if (!read.HasRows)
                    return null;
                while (read.Read())
                {
                    u.Password = read["PASSWORD"].ToString();
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM PERSONALIZATION WHERE USERNAME='" + username + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    u.Theme = read["THEME"].ToString();
                    u.TagTop = read["TAG_TOP"].ToString();
                    u.TagLeft = read["TAG_LEFT"].ToString();
                    u.TagWidth = read["TAG_WIDTH"].ToString();

                    u.ThreadTop = read["THREAD_TOP"].ToString();
                    u.ThreadLeft = read["THREAD_LEFT"].ToString();
                    u.ThreadWidth = read["THREAD_WIDTH"].ToString();

                    u.HeaderHeight = read["HEADER_HEIGHT"].ToString();
                    u.Locale = read["LOCALE"].ToString();
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM NAMESPACES WHERE USERNAME='" + username + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    u.namespaces.Add(read["NAMESPACE_ID"].ToString());
                }
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM ADMINS";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    if (read["USERNAME"].ToString().Equals(u.Username))
                        u.type = UserDTO.UserType.ADMIN;
                    else
                        u.type = UserDTO.UserType.USER;
                }
            }
            return u;
        }

        public UserDTO FindByEmail(string Email)
        {
            string username = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT USERNAME FROM PEOPLE WHERE EMAIL='" + Email + "'";
                con.Open();
                object o = cmd.ExecuteScalar();
                if (o == null)
                    return null;
                username = o.ToString();
            }

            return Find(username);
        }

        public List<UserDTO> GetAllUsers()
        {
            List<string> usernames = new List<string>();
            List<UserDTO> users = new List<UserDTO>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT USERNAME FROM LOGINS";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                    usernames.Add(read["USERNAME"].ToString());
            }

            foreach (string s in usernames)
                users.Add(Find(s));

            return users;
        }
    }
}