/*****************************************************************************
 * This file contains a C# class that represents an email account added to system.
 * Analogous to namespace in InboxAPI.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 03/09/2014
 *
******************************************************************************/

using System;

namespace TheMailClient.Domain.Model
{
    public class SyncAccount : IComparable<SyncAccount>
    {
        public SyncAccount()
        {
        }

        public SyncAccount(string email)
        {
            this.Email = email;
        }

        public string Id { get; internal set; }

        public string Email { get; internal set; }

        public string Namespace { get; internal set; }

        public string Account { get; internal set; }

        public string Provider { get; internal set; }

        public int CompareTo(SyncAccount obj)
        {
            return this.Email.CompareTo(obj.Email);
        }

        override public string ToString()
        {
            string str = String.Empty;
            str = String.Concat(str, "id = ", Id, "\r\n");
            str = String.Concat(str, "email = ", Email, "\r\n");
            str = String.Concat(str, "_namespace = ", Namespace, "\r\n");
            str = String.Concat(str, "account = ", Account, "\r\n");
            str = String.Concat(str, "provider = ", Provider, "\r\n");
            return str;
        }
    }
}