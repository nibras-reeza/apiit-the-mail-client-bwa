using System;
using System.Collections.Generic;

namespace TheMailClient.Domain.Model
{
    public class Contact
    {
        public string ID { get; set; }

        public string StreetAddress { get; set; }

        public class Name
        {
            public string FirstName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            override public string ToString()
            {
                string str = String.Empty;
                str = String.Concat(str, "FirstName = ", FirstName, "\r\n");
                str = String.Concat(str, "MiddleName = ", MiddleName, "\r\n");
                str = String.Concat(str, "LastName = ", LastName, "\r\n");
                return str;
            }
        }

        public Contact()
        {
            name = new Name();
            SecondaryEmails = new List<string>();
            SecondaryPhones = new List<string>();
            Tags = new List<Tag>();
        }

        public Name name { get; private set; }

        public string PrimayEmail { get; set; }

        public List<string> SecondaryEmails { get; private set; }

        public string PrimaryPhone { get; set; }

        public List<string> SecondaryPhones { get; private set; }

        public string Notes { get; set; }

        public List<Tag> Tags { get; private set; }
    }
}