using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMailClient.Domain.Model;
using TheMailClient.Domain.Model.ObjectFactory;
using TheMailClient.Storage;
using TheMailClient.Storage.DTO;

namespace TheMailClient.Domain.Services
{
    public class ContactService
    {
        private static ContactService instance = new ContactService();

        private ContactService()
        {
        }

        public static ContactService getInstance()
        {
            return instance;
        }

        public Contact getContact(string id, User u)
        {
            return Utils.Utils.getInstance().toDomain(ContactDataStore.getInstance().getContact(id, u.Username));
        }

        public Contact getContactByEmail(string email, User u)
        {
            return Utils.Utils.getInstance().toDomain(ContactDataStore.getInstance().getContactByEmail(email, u.Username));
        }

        public Contact getContactByName(string name, User u)
        {
            return Utils.Utils.getInstance().toDomain(ContactDataStore.getInstance().getContact(name, u.Username));
        }

        public List<Contact> getAllContacts(User u)
        {
            List<Contact> cons = new List<Contact>();

            foreach (ContactDTO d in ContactDataStore.getInstance().getAllContacts(u.Username))
                cons.Add(Utils.Utils.getInstance().toDomain(d));

            return cons;
        }

        public Contact update(Contact contact, User u)
        {
            ContactDTO dt = Utils.Utils.getInstance().toDTO(contact);

            return Utils.Utils.getInstance().toDomain(ContactDataStore.getInstance().update(dt, u.Username));
        }

        public Contact Add(Contact contact, User u)
        {
            HashingService.getInstance().GenerateId(u, contact);
            ContactDTO dt = Utils.Utils.getInstance().toDTO(contact);

            ContactDataStore.getInstance().Add(dt, u.Username);

            return getContact(contact.ID, u);
        }

        public void DeleteTag(Tag t)
        {
            ContactDataStore.getInstance().DeleteTag(t.Id);
        }

        public List<Contact> getAllContacts(Tag t, User u)
        {
            List<Contact> cons = new List<Contact>();

            foreach (ContactDTO d in ContactDataStore.getInstance().getContactsByTag(t.Id, u.Username))
                cons.Add(Utils.Utils.getInstance().toDomain(d));

            return cons;
        }

        public Contact Delete(Contact c, User u)
        {
            return Utils.Utils.getInstance().toDomain(ContactDataStore.getInstance().Delete(c.ID, u.Username));
        }

        public List<Tag> GetAllTags(User u)
        {
            List<Tag> l = new List<Tag>();

            foreach (TagDTO t in ContactDataStore.getInstance().getAllTags(u.Username))
                l.Add(Utils.Utils.getInstance().toDomain(t));

            return l;
        }

        public Tag GetTag(string query, User u)
        {
            foreach (TagDTO t in ContactDataStore.getInstance().getAllTags(u.Username))

                if (t.id.Equals(query) || t.name.Equals(query))

                    return Utils.Utils.getInstance().toDomain(t);

            TagFactory tf = new TagFactory();
            tf.Name = query;
            return tf.Create();
        }
    }
}