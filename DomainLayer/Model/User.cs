/*****************************************************************************
 * This file contains a C# class that represents an user of the system.
 *
 * Author: Nibras Ahamed Reeza (CB004641)
 * E-Mail: nibras.ahamed@gmail.com
 *
 * Last Modified: 03/09/2014
 *
******************************************************************************/

using System.Collections.Generic;

namespace TheMailClient.Domain.Model
{
    public class User : Person
    {
        public User()
        {
            Config = new PersonalizationConfig();
            accounts = new List<SyncAccount>();
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public PersonalizationConfig Config { get; private set; }

        public List<SyncAccount> accounts { get; private set; }

        public SyncAccount getAccount(string email)
        {
            SyncAccount a;

            int index = accounts.BinarySearch(new SyncAccount(email));

            if (index < 0)
                a = null;
            else
                a = accounts[index];

            return a;
        }

        public bool removeAccount(string email)
        {
            return accounts.Remove(getAccount(email));
        }
    }
}