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
    public class ContactDataStore
    {
        //https://stackoverflow.com/questions/6536715/get-connection-string-from-app-config
        private static ContactDataStore instance = new ContactDataStore();

        private static string connectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"C:\\Users\\Nibras\\Dropbox\\APIIT\\Final Year\\BWA\\final\\final\\Project\\TheMailClient\\StorageLayer\\App_Data\\TMCDB.mdf\";Integrated Security=True";

        private ContactDataStore()
        {
        }

        public static ContactDataStore getInstance()
        {
            return instance;
        }

        //http://msdn.microsoft.com/en-us/library/d7125bke.aspx
        // http://msdn.microsoft.com/en-us/library/fksx3b4f.aspx
        //http://www.codeproject.com/Articles/4416/Beginners-guide-to-accessing-SQL-Server-through-C

        public ContactDTO getContact(string contactId, string username)
        {
            ContactDTO obj = new ContactDTO();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CONTACT WHERE CONTACT_ID='" + contactId + "' AND USERNAME='" + username + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    obj.FirstName = res["FIRST_NAME"].ToString();
                    obj.LastName = res["LAST_NAME"].ToString();
                    obj.ID = res["CONTACT_ID"].ToString();
                    obj.MiddleName = res["MIDDLE_NAME"].ToString();
                    obj.Notes = res["NOTES"].ToString();
                    obj.PrimaryPhone = res["PHONE"].ToString();
                    obj.PrimayEmail = res["EMAIL"].ToString();
                    obj.StreetAddress = res["ADDRESS"].ToString();

                    obj.SecondaryEmails.AddRange(getEmails(obj.ID));
                    obj.SecondaryPhones.AddRange(getPhones(obj.ID));

                    obj.Tags.AddRange(getTags(obj.ID));
                }
            }
            return obj;
        }

        public ContactDTO getContactByEmail(string email, string username)
        {
            ContactDTO obj = new ContactDTO();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CONTACT WHERE EMAIL='" + email + "' AND USERNAME='" + username + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    obj.FirstName = res["FIRST_NAME"].ToString();
                    obj.LastName = res["LAST_NAME"].ToString();
                    obj.ID = res["CONTACT_ID"].ToString();
                    obj.MiddleName = res["MIDDLE_NAME"].ToString();
                    obj.Notes = res["NOTES"].ToString();
                    obj.PrimaryPhone = res["PHONE"].ToString();
                    obj.PrimayEmail = res["EMAIL"].ToString();
                    obj.StreetAddress = res["ADDRESS"].ToString();
                    obj.SecondaryEmails.AddRange(getEmails(obj.ID));
                    obj.SecondaryPhones.AddRange(getPhones(obj.ID));

                    obj.Tags.AddRange(getTags(obj.ID));
                }
            }
            return obj;
        }

        public ContactDTO getContactByName(string name, string username)
        {
            ContactDTO obj = new ContactDTO();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CONTACT WHERE ";

                foreach (string s in name.Split(' '))
                {
                    cmd.CommandText += "FIRST_NAME='" + s + "' OR";
                    cmd.CommandText += "MIDDLE_NAME='" + s + "' OR ";
                    cmd.CommandText += "LAST_NAME='" + s + "'";
                }

                cmd.CommandText += "' AND USERNAME='" + username + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    obj.FirstName = res["FIRST_NAME"].ToString();
                    obj.LastName = res["LAST_NAME"].ToString();
                    obj.ID = res["CONTACT_ID"].ToString();
                    obj.MiddleName = res["MIDDLE_NAME"].ToString();
                    obj.Notes = res["NOTES"].ToString();
                    obj.PrimaryPhone = res["PHONE"].ToString();
                    obj.PrimayEmail = res["EMAIL"].ToString();
                    obj.StreetAddress = res["ADDRESS"].ToString();
                    obj.SecondaryEmails.AddRange(getEmails(obj.ID));
                    obj.SecondaryPhones.AddRange(getPhones(obj.ID));

                    obj.Tags.AddRange(getTags(obj.ID));
                }
            }
            return obj;
        }

        public List<ContactDTO> getAllContacts(string username)
        {
            List<ContactDTO> list = new List<ContactDTO>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM CONTACT WHERE USERNAME='" + username + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    ContactDTO obj = new ContactDTO();
                    obj.FirstName = res["FIRST_NAME"].ToString();
                    obj.LastName = res["LAST_NAME"].ToString();
                    obj.ID = res["CONTACT_ID"].ToString();
                    obj.MiddleName = res["MIDDLE_NAME"].ToString();
                    obj.Notes = res["NOTES"].ToString();
                    obj.PrimaryPhone = res["PHONE"].ToString();
                    obj.PrimayEmail = res["EMAIL"].ToString();
                    obj.StreetAddress = res["ADDRESS"].ToString();

                    obj.SecondaryEmails.AddRange(getEmails(obj.ID));
                    obj.SecondaryPhones.AddRange(getPhones(obj.ID));

                    obj.Tags.AddRange(getTags(obj.ID));

                    list.Add(obj);
                }
            }

            return list;
        }

        public ContactDTO update(ContactDTO contact, string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UPDATE_CONTACT";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", username));
                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contact.ID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", contact.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", contact.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", contact.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", contact.StreetAddress));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", contact.PrimayEmail));
                cmd.Parameters.Add(new SqlParameter("@PHONE", contact.PrimaryPhone));
                cmd.Parameters.Add(new SqlParameter("@NOTES", contact.Notes));

                List<string> ServerPhones = getPhones(contact.ID);
                List<string> ServerEmails = getEmails(contact.ID);
                List<string> ServerTags = getTags(contact.ID);

                foreach (string s in (ServerPhones.Except(contact.SecondaryPhones)))
                    RemovePhone(contact.ID, s);

                foreach (string s in (ServerEmails.Except(contact.SecondaryEmails)))
                    RemoveEmail(contact.ID, s);

                foreach (string s in (ServerTags.Except(contact.Tags)))
                    RemoveTag(contact.ID, s);

                foreach (string s in (contact.SecondaryPhones.Except(ServerPhones)))
                    AddPhone(contact.ID, s);

                foreach (string s in (contact.SecondaryEmails.Except(ServerEmails)))
                    AddEmail(contact.ID, s);

                foreach (string s in (contact.Tags.Except(ServerTags)))
                    AddTag(contact.ID, s);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return getContact(contact.ID, username);
        }

        public void Add(ContactDTO contact, string username)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "INSERT_CONTACT";
                cmd.Parameters.Add(new SqlParameter("@USERNAME", username));
                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contact.ID));
                cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", contact.FirstName));
                cmd.Parameters.Add(new SqlParameter("@MIDDLE_NAME", contact.MiddleName));
                cmd.Parameters.Add(new SqlParameter("@LAST_NAME", contact.LastName));
                cmd.Parameters.Add(new SqlParameter("@ADDRESS", contact.StreetAddress));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", contact.PrimayEmail));
                cmd.Parameters.Add(new SqlParameter("@PHONE", contact.PrimaryPhone));
                cmd.Parameters.Add(new SqlParameter("@NOTES", contact.Notes));

                con.Open();
                cmd.ExecuteNonQuery();

                foreach (string s in contact.Tags)
                    AddTag(contact.ID, s);

                foreach (string s in contact.SecondaryPhones)
                    AddPhone(contact.ID, s);

                foreach (string s in contact.SecondaryEmails)
                    AddEmail(contact.ID, s);
            }
        }

        private List<string> getTags(string contactID)
        {
            List<string> tags = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TAG FROM CONTACT_TAG WHERE CONTACT_ID='" + contactID + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    tags.Add(res["TAG"].ToString());
                }
            }

            return tags;
        }

        public List<TagDTO> getAllTags(string username)
        {
            List<TagDTO> tags = new List<TagDTO>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TAG FROM CONTACT_TAG WHERE CONTACT_ID IN(SELECT CONTACT_ID FROM CONTACT WHERE USERNAME='" + username + "')";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    TagDTO t = new TagDTO();
                    t.id = res["TAG"].ToString();
                    t.name = t.id;
                    tags.Add(t);
                }
            }

            return tags;
        }

        private List<string> getEmails(string contactID)
        {
            List<string> emails = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT EMAIL FROM EMAILS WHERE CONTACT_ID='" + contactID + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    emails.Add(res["EMAIL"].ToString());
                }
            }

            return emails;
        }

        private List<string> getPhones(string contactID)
        {
            List<string> phones = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT PHONE FROM PHONES WHERE CONTACT_ID='" + contactID + "'";
                con.Open();
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    phones.Add(res["PHONE"].ToString());
                }
            }

            return phones;
        }

        private void AddTag(string contactID, string tag)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ADD_CONTACT_TAG";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@TAG", tag));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void RemoveTag(string contactID, string tag)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REMOVE_TAG";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@TAG", tag));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTag(string tag)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DELETE_TAG";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", tag));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void AddPhone(string contactID, string phone)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ADD_SECONDARY__PHONES";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@PHONE", phone));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void RemovePhone(string contactID, string phone)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REMOVE_SECONDARY__PHONE";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@PHONE", phone));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void AddEmail(string contactID, string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ADD_SECONDARY_EMAILS";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void RemoveEmail(string contactID, string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "REMOVE_SECONDARY__EMAIL";

                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                cmd.Parameters.Add(new SqlParameter("@EMAIL", email));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ContactDTO> getContactsByTag(string tag, string username)
        {
            List<ContactDTO> l = new List<ContactDTO>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT CONTACT_ID FROM CONTACT_TAG WHERE TAG='" + tag + "'";
                con.Open();
                SqlDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    l.Add(getContact(read["CONTACT_ID"].ToString(), username));
                }
            }
            return l;
        }

        public ContactDTO Delete(string contactID, string username)
        {
            ContactDTO dto = getContact(contactID, username);

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DELETE_CONTACT";
                cmd.Parameters.Add(new SqlParameter("@CONTACT_ID", contactID));
                con.Open();
                cmd.ExecuteNonQuery();
            }

            return dto;
        }
    }
}