using System;

namespace TheMailClient.Storage.DTO
{
    public class FileDTO
    {
        public string Namspace_ID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Id { get; set; }

        public long Size { get; set; }

        public string URL
        {
            get
            {
                return "http://5.231.64.215/n/" + Namspace_ID + "/files/" + Id + "/download";
            }
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "Namspace_ID = ", Namspace_ID, "\r\n");
            str = String.Concat(str, "Name = ", Name, "\r\n");
            str = String.Concat(str, "Type = ", Type, "\r\n");
            str = String.Concat(str, "Id = ", Id, "\r\n");
            str = String.Concat(str, "Size = ", Size, "\r\n");
            return str;
        }
    }
}