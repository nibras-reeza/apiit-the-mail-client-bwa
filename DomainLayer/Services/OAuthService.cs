using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Domain.Model;
using TheMailClient.Storage;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Services
{
    public class OAuthService
    {
        private static OAuthService instance = new OAuthService();

        private OAuthService()
        {
        }

        public static OAuthService GetInstance()
        {
            return instance;
        }

        public string GetAuthURL(string email)
        {
            return OAuthHandler.GetInstance().GetAuthURL(email);
        }

        public SyncAccount Authenticate(string email, string code)
        {
            AccountDTO dt = OAuthHandler.GetInstance().Authenticate(email, code);

            OAuthHandler.GetInstance().StartSync();

            return Utils.Utils.getInstance().toDomain(dt);
        }
    }
}