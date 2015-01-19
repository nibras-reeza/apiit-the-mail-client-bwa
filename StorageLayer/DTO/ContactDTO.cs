using System;
using System.Collections.Generic;

namespace TheMailClient.Storage.DTO
{
    public class ContactDTO
    {
        public string StreetAddress { get; set; }

        public string ID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public ContactDTO()
        {
            SecondaryEmails = new List<string>();
            SecondaryPhones = new List<string>();
            Tags = new List<string>();
        }

        public string PrimayEmail { get; set; }

        public List<string> SecondaryEmails { get; private set; }

        public string PrimaryPhone { get; set; }

        public List<string> SecondaryPhones { get; private set; }

        public List<string> Tags { get; private set; }

        public string Notes { get; set; }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "StreetAddress = ", StreetAddress, "\r\n");
            str = String.Concat(str, "ID = ", ID, "\r\n");
            str = String.Concat(str, "FirstName = ", FirstName, "\r\n");
            str = String.Concat(str, "MiddleName = ", MiddleName, "\r\n");
            str = String.Concat(str, "LastName = ", LastName, "\r\n");
            str = String.Concat(str, "PrimayEmail = ", PrimayEmail, "\r\n");
            str = String.Concat(str, "SecondaryEmails = ", SecondaryEmails, "\r\n");
            str = String.Concat(str, "PrimaryPhone = ", PrimaryPhone, "\r\n");
            str = String.Concat(str, "SecondaryPhones = ", SecondaryPhones, "\r\n");
            str = String.Concat(str, "Tags = ", Tags, "\r\n");
            str = String.Concat(str, "Notes = ", Notes, "\r\n");
            return str;
        }
    }
}