using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMailClient.Storage.DTO
{
    public class TagDTO
    {
        public string name { get; set; }
        public string id { get; set; }
        public string _namespace { get; set; }

        public int CompareTo(TagDTO obj)
        {
            return this.id.CompareTo(obj.id);
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "name = ", name, "\r\n");
            str = String.Concat(str, "id = ", id, "\r\n");
            str = String.Concat(str, "account = ", _namespace, "\r\n");
            return str;
        }
    }
}
