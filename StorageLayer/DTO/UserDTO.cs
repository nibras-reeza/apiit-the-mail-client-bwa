/*****************************************************************************
 * This file contains a C# class that represents an user of the system.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 03/09/2014
 *
******************************************************************************/

using System;
using System.Collections.Generic;

namespace TheMailClient.Storage.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
            namespaces = new List<string>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string TagLeft { get; set; }

        public string TagTop { get; set; }

        public string ThreadLeft { get; set; }

        public string ThreadTop { get; set; }

        public string TagWidth { get; set; }

        public string TagHeight { get; set; }

        public string ThreadWidth { get; set; }

        public string ThreadHeight { get; set; }

        public string HeaderLeft { get; set; }

        public string HeaderTop { get; set; }

        public string HeaderWidth { get; set; }

        public string HeaderHeight { get; set; }

        public string Theme { get; set; }

        public string Locale { get; set; }

        public List<string> namespaces { get; internal set; }

        public enum UserType { ADMIN, USER }

        public UserType type { get; set; }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "FirstName = ", FirstName, "\r\n");
            str = String.Concat(str, "LastName = ", LastName, "\r\n");
            str = String.Concat(str, "Address = ", Address, "\r\n");
            str = String.Concat(str, "EMail = ", EMail, "\r\n");
            str = String.Concat(str, "Phone = ", Phone, "\r\n");
            str = String.Concat(str, "Username = ", Username, "\r\n");
            str = String.Concat(str, "Password = ", Password, "\r\n");
            str = String.Concat(str, "TagLeft = ", TagLeft, "\r\n");
            str = String.Concat(str, "TagTop = ", TagTop, "\r\n");
            str = String.Concat(str, "ThreadLeft = ", ThreadLeft, "\r\n");
            str = String.Concat(str, "ThreadTop = ", ThreadTop, "\r\n");
            str = String.Concat(str, "TagWidth = ", TagWidth, "\r\n");
            str = String.Concat(str, "TagHeight = ", TagHeight, "\r\n");
            str = String.Concat(str, "ThreadWidth = ", ThreadWidth, "\r\n");
            str = String.Concat(str, "ThreadHeight = ", ThreadHeight, "\r\n");
            str = String.Concat(str, "HeaderLeft = ", HeaderLeft, "\r\n");
            str = String.Concat(str, "HeaderTop = ", HeaderTop, "\r\n");
            str = String.Concat(str, "HeaderWidth = ", HeaderWidth, "\r\n");
            str = String.Concat(str, "HeaderHeight = ", HeaderHeight, "\r\n");
            str = String.Concat(str, "Theme = ", Theme, "\r\n");
            str = String.Concat(str, "Locale = ", Locale, "\r\n");
            str = String.Concat(str, "namespaces = ", namespaces, "\r\n");
            str = String.Concat(str, "type = ", type, "\r\n");
            return str;
        }
    }
}