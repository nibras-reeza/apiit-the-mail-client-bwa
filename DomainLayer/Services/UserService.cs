using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Utils;
using TheMailClient.Storage;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Services
{
    public class UserService
    {
        private static UserService instance = new UserService();

        private UserService()
        {
        }

        public static UserService getInstance()
        {
            return instance;
        }

        // Authentication is done in business layer. Presentation has no access
        // to hashing algorithm.
        public User Authenticate(string username, string password)
        {
            UserDTO u = UserDataStore.getInstance().Find(username);
            if (u == null)
            {
                u = UserDataStore.getInstance().FindByEmail(username);
            }

            if (u == null)
                return null;

            if (!u.Password.Equals(HashingService.getInstance().HashPassword(password)))
                return null;

            return Utils.Utils.getInstance().toDomain(u);
        }

        public string Add(User u)
        {
            UserDTO dt = Utils.Utils.getInstance().toDTO(u);

            string s = string.Empty;
            s = UserDataStore.getInstance().Add(dt);

            return s;
        }

        public User Find(string username)
        {
            UserDTO u = UserDataStore.getInstance().Find(username);

            return Utils.Utils.getInstance().toDomain(u);
        }

        public User Update(User u)
        {
            UserDTO dto = Utils.Utils.getInstance().toDTO(u);

            dto = UserDataStore.getInstance().Update(dto);

            return Utils.Utils.getInstance().toDomain(dto);
        }

        public User Delete(User u)
        {
            UserDTO dto = UserDataStore.getInstance().Delete(u.Username);

            return Utils.Utils.getInstance().toDomain(dto);
        }

        public User Delete(string username)
        {
            UserDTO dto = UserDataStore.getInstance().Delete(username);

            return Utils.Utils.getInstance().toDomain(dto);
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            foreach (UserDTO dt in UserDataStore.getInstance().GetAllUsers())
                users.Add(Utils.Utils.getInstance().toDomain(dt));

            return users;
        }
    }
}