using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMailClient.Storage.DTO
{
    public class AccountDTO : IComparable<AccountDTO>
    {
        public AccountDTO() { }
        public AccountDTO(string email)
        {
            this.email = email;
        }

        public string id { get; set; }
        public string email { get; set; }
        public string _namespace { get; set; }
        public string account { get; set; }
        public string provider { get; set; }

        public int CompareTo(AccountDTO obj)
        {
            return this.email.CompareTo(obj.email);
        }
        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "id = ", id, "\r\n");
            str = String.Concat(str, "email = ", email, "\r\n");
            str = String.Concat(str, "_namespace = ", _namespace, "\r\n");
            str = String.Concat(str, "account = ", account, "\r\n");
            str = String.Concat(str, "provider = ", provider, "\r\n");
            return str;
        }
    }
}
