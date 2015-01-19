using System;

namespace TheMailClient.Domain.Model
{
    public class Tag : IComparable<Tag>
    {
        public string Name { get; set; }

        public string Id { get; internal set; }

        public SyncAccount Account { get; internal set; }

        public int CompareTo(Tag obj)
        {
            return this.Id.CompareTo(obj.Id);
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "name = ", Name, "\r\n");
            str = String.Concat(str, "id = ", Id, "\r\n");
            str = String.Concat(str, "account = ", Account, "\r\n");
            return str;
        }
    }
}